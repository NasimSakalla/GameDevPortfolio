using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEngine : MonoBehaviour
{
    private int speed = 5; // Movement speed of the target

    [SerializeField] private Rigidbody targetRB; // Rigidbody component of the target
    [SerializeField] private GameObject player; // Reference to the player GameObject

    private void Start()
    {
        InitializeVars();
    }
    private void InitializeVars()
    {
        speed = 5;
    }
    public void Movement(string dir)
    {
        int xAxis = 0, yAxis = 0; 

        // Determine direction based on input string
        if (dir == "Right")
            xAxis = 1;
        else if (dir == "Left")
            xAxis = -1;
        else if (dir == "Up")
            yAxis = 1;
        else if (dir == "Down")
            yAxis = -1;
        else if (dir == "Stop") // Stop movement if "Stop" is called
        {
            targetRB.velocity = Vector3.zero;
            return; // Exit function early
        }

        // Apply calculated movement to the Rigidbody
        targetRB.velocity = new Vector3(xAxis * speed, yAxis * speed, 0);
    }

    // Launches the ball towards the target when the player kicks
    public void LaunchBall()
    {
        BallManager ballManager = FindObjectOfType<BallManager>(); // Getting a reference to BallManager
        ballManager.didPlayerKick = true;  // Directly updating BallManager's didPlayerKick
        int launchSpeed = 5000; // Force applied to the ball

        // Calculate direction from the player to the target
        Vector3 dirOfShot = (targetRB.position - player.transform.position).normalized;

        // Apply force to the player's Rigidbody to simulate the shot
        player.GetComponent<Rigidbody>().AddForce(dirOfShot * launchSpeed);
    }
}
