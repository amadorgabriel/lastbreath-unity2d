using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator animator;
    private float transitionTime = 0.56f;


    public void PlayGame(){
        Debug.Log("play");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    //Coroutine
    IEnumerator LoadLevel(int levelIndex){
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
