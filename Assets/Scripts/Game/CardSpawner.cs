using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    //A list of spawnpoints
    [SerializeField] List<Vector3> spawnPoints;
    //A list of the card prefabs
    [SerializeField] List<GameObject> cardPrefabs;

    private void Start()
    {
        spawnCards();
    }

    void spawnCards()
    {
        GameObject temp = null;
        int pickedPosNumber = 0;
        int pickedCardNumber = 0;
        int amountOfSpawnPoints = 15;

        for (int j = 0; j < 5 ; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                //pick a random number in our range
                pickedPosNumber = Random.Range(0, amountOfSpawnPoints);
                //make temp hold our created card
                temp = Instantiate(cardPrefabs[pickedCardNumber]);
                //move temp to our desried pos
                temp.transform.position = spawnPoints[pickedPosNumber];
                //remove the picked position 
                spawnPoints.RemoveAt(pickedPosNumber);
                //reduce the amount of spawn points after spawing a card
                amountOfSpawnPoints--;
            }
            pickedCardNumber++;
        }
    }

}
