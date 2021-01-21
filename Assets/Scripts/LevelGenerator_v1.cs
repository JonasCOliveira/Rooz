using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator_v1 : MonoBehaviour
{

    private const float END_OF_PHASE = 200f;
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 5f;
    public float PLAYER_DISTANCE_UNTIL_THE_END = 100f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private CharacterController2D player;

    private bool gameIsOver = false;

    private Vector3 lastStartPosition;
    private Vector3 lastEndPosition;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private float maxAxisXWidht, minAxisXWidht, maxAxisXChange, widthChange;

    private CoinGenerator theCoinGenerator;
    private bool gameReady = false;
    public float randomCoinThreshold;
    private float spawnPositionCoin;

    public bool HighLow = false;

    private void Awake()
    {
        maxAxisXChange = .5f;
        minAxisXWidht = 0.5f;
        maxAxisXWidht = 2f;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
        randomCoinThreshold = 25f;
        gameReady = false;

        maxHeightChange = 1f;
        PLAYER_DISTANCE_UNTIL_THE_END = 150f;
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        gameIsOver = false;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        lastStartPosition = levelPart_Start.Find("StartPosition").position;
        int startingSpawnLevelParts = 2;

        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
        gameReady = true;
    }
    
    private void Update()
    {
        float distanceFromEnd = Vector3.Distance(player.transform.position, lastEndPosition);

        if (Random.value >= 0.5){
            gameReady = true;
        }
        else {
            gameReady = false;
        }

        // Debug.Log("LastEndPosition value: " + lastEndPosition.x);
        //Debug.Log("Player: " + player.transform.position + " || End Position: " + lastEndPosition + " || Distancia: " + distanceFromEnd);
        if (!gameIsOver)
        {
            if (lastEndPosition.x >= PLAYER_DISTANCE_UNTIL_THE_END)
            {
                SpawnLastLevelPart();
                gameIsOver = true;
            }
            else
            {
                //if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
                if ((lastEndPosition.x - player.transform.position.x) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
                {
                    // Spawn another level part
                    SpawnLevelPart();
                }
            }
        }

        if(lastEndPosition.x >= END_OF_PHASE){

                SpawnFinalPart();

        }

    }

    private void PlatformHeight()
    {
        //Testing x variation
        widthChange = Random.Range(maxAxisXWidht, minAxisXWidht);
        if(widthChange > maxAxisXWidht)
        {
            widthChange = maxAxisXWidht;
        }
        else
        {
            if(widthChange < minAxisXWidht)
            {
                widthChange = minAxisXWidht;
            }
        }


        float randomNumber = Random.Range(maxHeightChange, -maxHeightChange);
        //int roundedValue = Mathf.RoundToInt(randomNumber);
        //Debug.Log("NUMERO RANDOM: " + randomNumber + " || VALOR DO rounded: " + roundedValue);
        // Debug.Log("NUMERO RANDOM: " + randomNumber);
        heightChange += randomNumber;
        //heightChange += roundedValue;

        if (heightChange > maxHeight)
        {
            heightChange = maxHeight;
        }
        else
        {
            if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }
        }

        // Debug.Log("Valor da altura: " + heightChange + "  |||| VALOR do X: " + widthChange);
    }

    private void SpawnLevelPart()
    {

        //Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count - 1)];
        //Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        PlatformHeight();
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count -1 )];
        Transform lastLevelPartTransform; 
        lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));

        //Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
        /*
        if (HighLow)
        {
            lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, lastEndPosition.y + maxHeightChange, lastEndPosition.z));
            }

        else
        {
            lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, lastEndPosition.y + maxHeightChange, lastEndPosition.z));
            HighLow = !HighLow;
        }
        */
        
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;
        
        if (gameReady)
        {
            if (Random.Range(0f, 30f) < randomCoinThreshold)
            {
                // Debug.Log("Coin respawn");
                spawnPositionCoin = lastStartPosition.x  + (lastEndPosition.x - lastStartPosition.x)/2;
                theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin + widthChange, heightChange + 0.8f, lastEndPosition.z));
            }
        }

        
        Debug.Log(lastStartPosition + " - " +  lastEndPosition);
    }

    private void SpawnLastLevelPart()
    {
            Transform chosenLevelPart = levelPartList[levelPartList.Count-1];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;
    }

    private void SpawnFinalPart(){

            Transform chosenLevelPart = levelPartList[levelPartList.Count];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;

    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
