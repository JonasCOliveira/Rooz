
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public static Animator anim;

    void Start(){

        anim = gameObject.GetComponent<Animator>();

    }

    public static void Reset(){

        
        if(GameMaster.instance.GetNumOfHearts() == 0){
            Debug.Log("Reset");
           SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        } 

        else {
             Debug.Log("Checkpoint");
             PlayerPos.LoadCheckPoint();
             GameMaster.instance.AttHud();

        }

    }

    public static void Hurt(){

        anim.SetBool("Hurting", true);

    }
}
