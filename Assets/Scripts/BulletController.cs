using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public GameObject hitEffect;

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag != "Bullet") {
            GameObject effect = Instantiate (hitEffect, transform.position, Quaternion.identity);
            Destroy (effect, 0.31f);
            Destroy (gameObject);
        }

    }

}