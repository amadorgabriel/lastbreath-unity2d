using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class IARobotController : MonoBehaviour {

    [Header ("Robot")]
    private Animator myAnimator;
    private AIPath aiPath;

    [Header ("Gun")]
    public float bulletForce;
    public Transform firePoint;
    public GameObject bullet_pfb;
    private Animator myAimAnimator;

    [Header ("Shoot")]
    public int numProjeteis; //bulletsAmount
    public float startAngle = 90f, endAngle = 270f;
    private Vector2 bulletMoveDirection;

    [Header ("Transforms")]
    private Transform aimTransform;
    public Transform playerTransform;

    void Start () {
        aimTransform = transform.Find ("Aim");
        myAnimator = GetComponent<Animator> ();
        myAimAnimator = aimTransform.Find ("MgPistol").GetComponent<Animator> ();
        aiPath = GetComponent<AIPath> ();

        // aiPath.canSearch = false;
    }

    void Update () {
        _LookAtPlayer ();
        _MoveEnemy ();
        _HandleAiming ();
        _HandleShooting ();
    }

    private void _MoveEnemy () {
        if (aiPath.desiredVelocity.x != 0 || aiPath.desiredVelocity.y != 0) {
            myAnimator.SetBool ("isMoving", true);
        } else {
            myAnimator.SetBool ("isMoving", false);
        }
    }

    private void _HandleAiming () {
        Vector3 playerPosition = playerTransform.position;

        Vector3 aimDirection = (playerPosition - transform.position).normalized;
        float angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3 (0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90f || angle < -90f) {
            aimLocalScale.y = -1f;
        } else {
            aimLocalScale.y = +1f;
        }
        aimTransform.localScale = aimLocalScale;
    }

    private void _HandleShooting () {

        if (1 == 2) {
            myAimAnimator.SetTrigger ("Shoot");

            float espaçametoBullet = (endAngle - startAngle) / numProjeteis;
            float angulo = startAngle;

            for (int i = 0; i < numProjeteis; i++) {

                float direcaoX = bullet_pfb.transform.position.x + Mathf.Sin ((angulo * Mathf.PI) / 180f);
                float direcaoY = bullet_pfb.transform.position.x + Mathf.Cos ((angulo * Mathf.PI) / 180f);

                Vector3 moveBullet = new Vector3 (direcaoX, direcaoY, 0f);
                Vector2 bulDir = (moveBullet - bullet_pfb.transform.position).normalized;

                GameObject bullet = Instantiate (bullet_pfb, firePoint.position, firePoint.rotation);
                Rigidbody2D bullet_Rb = bullet.GetComponent<Rigidbody2D> ();
                bullet_Rb.AddForce (firePoint.right * bulletForce, ForceMode2D.Impulse);

                angulo += espaçametoBullet;

            }

        }
    }

    private void _LookAtPlayer () {
        Vector3 playerPosition = playerTransform.position;

        //Virar o player
        if (playerPosition.y >= 4f) {
            myAnimator.SetFloat ("moveY", 1f);
        } else if (playerPosition.y <= -4f) {
            myAnimator.SetFloat ("moveY", -1f);
        } else {
            myAnimator.SetFloat ("moveY", 0f);
        }

        if (playerPosition.x >= transform.position.x) {
            myAnimator.SetFloat ("moveX", 1f);
        } else if (playerPosition.x <= transform.position.x) {
            myAnimator.SetFloat ("moveX", -1f);
        } else {
            myAnimator.SetFloat ("moveX", 0f);
        }
    }
}