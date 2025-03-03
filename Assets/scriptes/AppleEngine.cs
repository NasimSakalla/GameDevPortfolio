using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleEngine : MonoBehaviour
{
    //teleports the apple to a new position 
    void MoveApple()         
    {
        float newX = Random.Range(-16f,17f);
        float newY = Random.Range(-8f,9f);
        Vector2 newApplePos = new(newX,newY);
        transform.position = newApplePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake"))
        {
            MoveApple();
        }
    }
}
