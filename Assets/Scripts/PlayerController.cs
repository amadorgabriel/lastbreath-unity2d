using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header ("Movement")]
    public int mySpeed;
    private Vector3 change;
    public float dashDuration;

    [Header ("Components")]
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private SpriteRenderer mySprite;
    private TrailRenderer myTrailRenderer;
    private ParticleSystem dust;

    void Start () {
        myRigidbody = GetComponent<Rigidbody2D> ();
        myAnimator = GetComponent<Animator> ();
        mySprite = GetComponent<SpriteRenderer> ();
        dust = transform.Find ("DustEffect").GetComponent<ParticleSystem> ();

        //Testing
        myTrailRenderer = transform.Find ("DashEffect").GetComponent<TrailRenderer> ();
    }

    void Update () {
        _MoveCharacter ();
        _DashCharacter ();
    }

    void _MoveCharacter () {
        //GetAxisRaw retorna brutamente 1, 0, -1
        change = Vector3.zero;
        change.x = Input.GetAxisRaw ("Horizontal");
        change.y = Input.GetAxisRaw ("Vertical");

        if (change != Vector3.zero) {
            myAnimator.SetFloat ("moveX", change.x);
            myAnimator.SetFloat ("moveY", change.y);
            myRigidbody.MovePosition (transform.position + change * mySpeed * Time.deltaTime);

            //Inverter Olhar
            // if(change.x == 1){
            //     transform.eulerAngles = new Vector3(0f, 180f, 0f);
            // }else if ( change.x == -1) {
            //     transform.eulerAngles = new Vector3(0f, 0f, 0f);
            // }

            myAnimator.SetBool ("isMoving", true);
        } else {
            myAnimator.SetBool ("isMoving", false);
        }

    }

    void _DashCharacter () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            _CreateDust ();
            myRigidbody.MovePosition (transform.position + change.normalized * 2f);
        }
    }

    void _CreateDust () {
        dust.Play ();
    }

}