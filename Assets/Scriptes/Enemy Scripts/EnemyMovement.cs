using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    void Update()
    {
        MoveEnemy();
    }
    string LeftOrRightToPlayer()
    {
        float playerXpos = GameObject.FindGameObjectWithTag("Player").transform.position.x;

        if (playerXpos > transform.position.x)
        {
            return "Left";

        }
        else 
        {
           return  "Right";
        }
    }
    void MoveEnemy()
    {
        if (LeftOrRightToPlayer() == "Left")
        {
            transform.Translate(0.015f, 0, 0);
        }
        if (LeftOrRightToPlayer() == "Right")
        {
            transform.Translate(-0.015f, 0, 0);
        }
    }
}