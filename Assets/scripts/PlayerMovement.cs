using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
	public  Animator animator;

    public float runSpeed = 40f;
	float horizontalMove = 0f;

	bool jump = false;
	bool crouch = false;

    // Update is called once per frame
    void Update()
    { 

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        

    
    	if (Input.GetButtonDown("Jump") ) {

			jump = true;
			animator.SetBool("isJumping", true);
		}

		if (Input.GetButtonDown("Crouch")){
			
			Debug.Log("Agacha");
			crouch = true;	

		} else if(Input.GetButtonUp("Crouch")){

			crouch = false;
		}
    
    }

	public void OnLanding() {

		animator.SetBool("isJumping", false);

	}

	public void OnCrounching(bool isCrouching){

		animator.SetBool("isCrouching", isCrouching);

	}


    void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

	void OnTriggerEnter2D(Collider2D trig){

         if(trig.CompareTag("enemy")){

             StartCoroutine(PlayerHurt());
       
        }
    }


     IEnumerator PlayerHurt(){

        animator.SetBool("Hurting", true);
        yield return new WaitForSeconds(0.2f);
        GameMaster.instance.SetNumOfHearts(-1);
        animator.SetBool("Hurting", false);
        Reset();
        

    }

	    public void Reset(){

        
        if(GameMaster.instance.GetNumOfHearts() == 0){
			
			GameMaster.instance.ResetNumOfHearts();
			GameMaster.instance.ResetNumOfPoints();
			GameMaster.instance.ResetNumOfBullets();
			GameMaster.instance.SetNumOfBullets(10);
			// GameMaster.instance.SetNumOfPoints();
			GameMaster.instance.SetNumOfHearts(5);
           	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
			GameMaster.instance.AttHud();
        } 

        else {
             GameMaster.instance.AttHud();

        }

    }


}
