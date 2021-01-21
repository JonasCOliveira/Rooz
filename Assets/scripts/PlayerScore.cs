using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

public CharacterController2D controller;
public static GameMaster instance;


private int score; 

private void Start() {

score = 0;

}

private void Update() {

if(controller){

    if(score <=  controller.transform.position.x){

        score = Mathf.FloorToInt(controller.transform.position.x);
        GameMaster.instance.SetScore(score);
        GameMaster.instance.AttHud();
    }

} else {

    score = 0;
    GameMaster.instance.SetScore(score);
    GameMaster.instance.AttHud();

}


}

void OnTriggerEnter2D(Collider2D other){

	if(other.CompareTag("gem")){

            GameMaster.instance.SetNumOfPoints(1);
        	GameMaster.instance.AttHud();
    }

    else if(other.CompareTag("life")){

            GameMaster.instance.SetNumOfHearts(1);
            GameMaster.instance.AttHud();
    }
 
    }

}
