using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Header("Player")]
    public Transform playerTransform;

    [Header("Camera")]
    private float velCamera = 3.5f;
    // private Camera camera;
    public Transform camera;
    private Transform limiteCamEsq, limiteCamDir, limiteCamCima, limiteCamBaixo;

    void Start () {
        // camera = Camera.main;
        limiteCamEsq = transform.Find("LimiteCamEsq");
        limiteCamDir = transform.Find("LimiteCamDir");
        limiteCamBaixo = transform.Find("LimiteCamInf");
        limiteCamCima = transform.Find("LimiteCamSup");
    }

    void LateUpdate () {

        //CAMERA
        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y + 0.25f;

        if (camera.transform.position.x < limiteCamEsq.position.x && playerTransform.position.x < limiteCamEsq.position.x) {
            posCamX = limiteCamEsq.position.x;
        } else if (camera.transform.position.x > limiteCamDir.position.x && playerTransform.position.x > limiteCamDir.position.x) {
            posCamX = limiteCamDir.position.x;
        }

        if (camera.transform.position.y > limiteCamCima.position.y && playerTransform.position.y > limiteCamCima.position.y) {
            posCamY = limiteCamCima.position.y;
        } else if (camera.transform.position.y < limiteCamBaixo.position.y && playerTransform.position.y < limiteCamBaixo.position.y) {
            posCamY = limiteCamBaixo.position.y;
        }

        Vector3 posCam = new Vector3 (posCamX, posCamY, camera.transform.position.z);
        camera.transform.position = Vector3.Lerp (camera.transform.position, posCam, velCamera * Time.deltaTime);
    }

}