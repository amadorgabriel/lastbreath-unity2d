 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour {


    [Header ("Gun")]
    public float bulletForce;
    public Transform firePoint;
    private Transform aimTransform;
    public GameObject bullet_pfb;

    [Header ("Components")]
    private Animator myPlayerAnimator;
    private Animator myAimAnimator;
    
    public CameraController cameraController;

    //UNITY FUNCTIONS
    private void Awake () {
        aimTransform = transform.Find ("Aim");
        myPlayerAnimator = GetComponent<Animator> ();
        myAimAnimator = aimTransform.Find ("Pistol").GetComponent<Animator> ();
    }

    void Update () {
        _HandleAiming ();
        _HandleShooting ();
    }

    //PERSONAL FUNCTIONS
    private void _HandleShooting () {
        Vector3 mousePosition = GetMouseWorldPosition ();

        if (Input.GetMouseButtonDown (0)) {
            myAimAnimator.SetTrigger ("Shoot");
            StartCoroutine(cameraController._ShakeCamera(.1f, .1f));

            GameObject bullet = Instantiate(bullet_pfb, firePoint.position, firePoint.rotation );
            Rigidbody2D bullet_Rb = bullet.GetComponent<Rigidbody2D>();
            bullet_Rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

        }
    }

    private void _HandleAiming () {
        Vector3 mousePosition = GetMouseWorldPosition ();

        // Direita do objeto representa o '0'
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3 (0, 0, angle);

        //Virar a pistola
        Vector3 aimLocalScale = Vector3.one; // (1, 1, 1)
        if (angle > 90f || angle < -90f) {
            aimLocalScale.y = -1f;
        } else {
            aimLocalScale.y = +1f;
        }
        aimTransform.localScale = aimLocalScale;

        //Virar o player
        if (mousePosition.y >= 4f) {
            myPlayerAnimator.SetFloat ("moveY", 1f);
        } else if (mousePosition.y <= -4f) {
            myPlayerAnimator.SetFloat ("moveY", -1f);
        } else {
            myPlayerAnimator.SetFloat ("moveY", 0f);
        }

        if (mousePosition.x >= transform.position.x) {
            myPlayerAnimator.SetFloat ("moveX", 1f);
        } else if (mousePosition.x <= transform.position.x) {
            myPlayerAnimator.SetFloat ("moveX", -1f);
        } else {
            myPlayerAnimator.SetFloat ("moveX", 0f);
        }

    }

    private static Vector3 GetMouseWorldPosition () {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }
}