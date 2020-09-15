using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RoomLightsController : MonoBehaviour {
    private bool roomUnlocked;
    private Light2D brilhoOpaco;
    private ParticleSystem particulasEffect;

    void Start () {
        brilhoOpaco = GetComponent<Light2D> ();
        particulasEffect = GetComponent<ParticleSystem> ();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (!roomUnlocked && other.tag == "Player") {
            brilhoOpaco.enabled = false;
            particulasEffect.Play ();
            roomUnlocked = true;
        }
    }
}