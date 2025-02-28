using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] List<Vector3> spawnPointsRight;
    [SerializeField] List<Vector3> spawnPointsLeft;
    [SerializeField] List<Vector3> spawnPointsUp;
    [SerializeField] List<Vector3> spawnPointsBottom;

    [SerializeField] List<GameObject> blockTypes;

    public static int spawnCount;

    private void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        //spawn left side
        for (int i = 0; i < spawnCount ; i++)
        {
            var temp1 = Instantiate(blockTypes[Random.Range(0, 4)] , spawnPointsLeft[Random.Range(0, 4)], Quaternion.identity);
            var temp2 = Instantiate(blockTypes[1] , spawnPointsLeft[Random.Range(0,4)] , Quaternion.identity);

            temp1.GetComponent<Rigidbody2D>().AddForce(new Vector2(500,0));
            temp2.GetComponent<Rigidbody2D>().AddForce(new Vector2(500,0));
        }
        //spawn right side
        for (int i = 0; i < spawnCount; i++)
        {
            var temp1 = Instantiate(blockTypes[Random.Range(0, 4)], spawnPointsRight[Random.Range(0, 4)], Quaternion.identity);
            var temp2 = Instantiate(blockTypes[1], spawnPointsRight[Random.Range(0, 4)], Quaternion.identity);

            temp1.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 0));
            temp2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 0));
        }
        //spawn up side
        for (int i = 0; i < spawnCount; i++)
        {
            var temp1 = Instantiate(blockTypes[Random.Range(0, 4)], spawnPointsUp[Random.Range(0, 4)], Quaternion.identity);
            var temp2 = Instantiate(blockTypes[1], spawnPointsUp[Random.Range(0, 4)], Quaternion.identity);

            temp1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -500));
            temp2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -500));
        }
        //spawn under side
        for (int i = 0; i < spawnCount; i++)
        {
            var temp1 = Instantiate(blockTypes[Random.Range(0, 4)], spawnPointsBottom[Random.Range(0, 4)], Quaternion.identity);
            var temp2 = Instantiate(blockTypes[1], spawnPointsBottom[Random.Range(0, 4)], Quaternion.identity);

            temp1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
            temp2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
        }
    }
}
