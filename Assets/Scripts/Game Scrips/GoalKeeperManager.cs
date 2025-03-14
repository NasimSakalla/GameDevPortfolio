using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeperManager : MonoBehaviour
{
    // Movement speed and direction variables
    private float keeperMovementAmount; // Speed of movement
    private float movementInterval; // Time in seconds before reversing direction
    private float moveDirection; // Keeps track of direction (1 for moving right, -1 for left)
    private float timer; // Timer to track the movement interval

    private void Start()
    {
        // Start the keeper movement loop when the game starts
        StartCoroutine(MoveKeeper());
        InitializeVars();

    }

    void InitializeVars()
    {
        keeperMovementAmount = 9f;
        movementInterval = 0.8f;
        moveDirection = 1f;
        timer = 0f;
    }


    // Coroutine for moving the goalkeeper
    IEnumerator MoveKeeper()
    {
        while (true)
        {
            // Move the keeper smoothly over time
            transform.Translate(keeperMovementAmount * moveDirection * Time.deltaTime, 0, 0);

            // Wait for 1 frame to continue the loop (next frame)
            yield return null;

            // Update the timer and check if the movement interval has passed
            timer += Time.deltaTime;
            if (timer >= movementInterval)
            {
                // Reverse the movement direction after the interval
                moveDirection *= -1f;
                timer = 0f; // Reset the timer
            }
        }
    }
}
