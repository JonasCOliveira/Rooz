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

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){

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
