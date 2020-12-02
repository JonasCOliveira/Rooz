using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{

    public static Animator anim;
    public float transitionTime = .5f;

    void Start(){

        // anim = gameObject.GetComponent<Animator>();

    }

    public static void Reset(){

        
        if(GameMaster.instance.GetNumOfHearts() == 0){
            Debug.Log("Reset");
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        } 

        else {
            //  PlayerPos.LoadCheckPoint();
             GameMaster.instance.AttHud();

        }

    }

	void OnTriggerEnter2D(Collider2D trig){

         if(trig.CompareTag("enemy")){

             StartCoroutine(PlayerHurt());
       
        }
    }


     IEnumerator PlayerHurt(){

        anim.SetBool("Hurting", true);
        yield return new WaitForSeconds(transitionTime);
        GameMaster.instance.SetNumOfHearts(-1);
        anim.SetBool("Hurting", false);
        Reset();
        
        

    }
}
