using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

     void OnTriggerEnter2D(Collider2D other){

	if(other.CompareTag("coins")){

            GameMaster.instance.SetNumOfPoints(1);
        	GameMaster.instance.AttHud();
    }
 
    }

}
