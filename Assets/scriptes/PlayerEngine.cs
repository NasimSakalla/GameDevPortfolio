using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerEngine : MonoBehaviour
{
    private GameObject snakeBodyPrefab;
    public int amountEaten;
    private int Xdir, Ydir;
    private float speed;

    void Start()
    {
        // Loads the prefab once
        snakeBodyPrefab = Resources.Load<GameObject>("Snake Body");
        if (snakeBodyPrefab == null)
        {
            Debug.LogError("Snake Body prefab not found!");
        }
        // Setting the speed
        speed = 4;
        // Resetting player position
        transform.position = new Vector2(0, 0);
    }

    void Update()
    {
        MovementAndRotation();
    }

    void MovementAndRotation()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            Xdir = 1;
            Ydir = 0;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            Xdir = -1;
            Ydir = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Xdir = 0;
            Ydir = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            Xdir = 0;
            Ydir = -1;
        }

        transform.position += speed * Time.deltaTime * new Vector3(Xdir, Ydir, 0);
    }

    void MakeSnakeBigger()
    {
        if (transform.childCount > 0) // Check if there are any child objects
        {
            int childCount = transform.childCount;
            float childYpos = transform.GetChild(childCount - 1).transform.localPosition.y;

            // Spawn new body part relative to the last one
            Vector3 spawningPosition = new Vector3(0, childYpos - 1.1f, 0);

            // Make the new body part a child of the snake's parent
            var temp = Instantiate(snakeBodyPrefab);
            temp.transform.SetParent(transform);
            temp.transform.rotation = Quaternion.identity;
            temp.transform.localScale = Vector3.one;        // Resets scale to (1,1,1)
            temp.transform.localPosition = spawningPosition;
        }
        else
        {
            Debug.LogWarning("No child segments to follow!");
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("apple"))
        {
            //adding blocks to the snakes body when he eats an apple
            MakeSnakeBigger();
            speed += 0.3f;
            amountEaten++;
        }
        if (collision.gameObject.CompareTag("parameter"))
        {
            //adding a kill barrier
            Destroy(gameObject);
        }
    }
}

