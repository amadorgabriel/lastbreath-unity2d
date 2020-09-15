using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Transform player;
    
    void LateUpdate()
    {
        Vector3 position = player.position;
        position.y = transform.position.y;
        // transform.position = position;
    }
}
