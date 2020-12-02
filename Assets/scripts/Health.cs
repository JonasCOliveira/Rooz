using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset(){


        if(GameMaster.instance.GetNumOfHearts() == 0){

           SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        } 

        else {
             PlayerPos.LoadCheckPoint();
             GameMaster.instance.AttHud();

        }

    }

}
