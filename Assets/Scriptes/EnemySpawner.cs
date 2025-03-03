using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int spawnAmount;
    [SerializeField] GameObject spaceshipEnemy;

    void Start()
    {
        spawnAmount = 2; // Start with 2 enemies
    }

    void Update()
    {
        AllowSpawn();
    }

    void AllowSpawn()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)     //check if any enemy in the heiarchy is tagged 
        {
            //if not spawn enemies
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(spaceshipEnemy);
        }
        //add one more next time calling the spawn function
        spawnAmount++;
    }
}
