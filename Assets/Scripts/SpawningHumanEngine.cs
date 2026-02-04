using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawningHumanEngine : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] public static int scoreOfPlayer;

    private void Start()
    {
        //after being created seek players position
        playerPos = GameObject.Find("Superman").transform;

        Invoke(nameof(SpawnHuman), 0.5f);
    }
    void SpawnHuman()
    {
        //setting a spawn point for our humans
        Vector3 spawnPoint = new Vector3(playerPos.position.x + Random.Range(-4,4), Random.Range(30,36) , playerPos.position.z + 35);
        //creating humans in said position
        Instantiate(gameObject, spawnPoint, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        //destroying human when he touches the floor
        if (other.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
        //destroying when it touches superman
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            scoreOfPlayer+=1000;
        }
    }
}
