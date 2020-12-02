using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    void OnTriggerEnter2D(Collider2D trig){

         if(trig.CompareTag("Player")){

             LoadNextLevel();
       
        }
    }


    public void LoadNextLevel(){

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        GameMaster.instance.AttHud();
    }

     IEnumerator  LoadLevel(int levelIndex){

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        

    }


}
