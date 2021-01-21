using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Animator anim;
    public float runSpeed = 10f;
    public bool isRight;

    void Update() {

        if(isRight){

            transform.Translate( - 1 *runSpeed * Time.deltaTime, 0,0);
            transform.localScale = new Vector2(2,2);
    
        } else{

            transform.Translate(  1 *  runSpeed * Time.deltaTime, 0,0);
            transform.localScale = new Vector2(-2,2);
    
        }

    }  

    void OnTriggerEnter2D(Collider2D trig){


        if(trig.gameObject.CompareTag("turnLeftRight")){

            if(isRight){
                isRight = false;
            }
            else {
                isRight = true;
            }

        }

        if(trig.gameObject.CompareTag("Bullet")){

            StartCoroutine(DestroyOpossum());
        }

    }

    IEnumerator DestroyOpossum(){

        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
        
    }


}
