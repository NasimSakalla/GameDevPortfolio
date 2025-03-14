using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Making an instance of this script so I can access these functions easily
    public static GameManager gameManagerInstance;
    public int goalsAiScored;

    // Game Components
    [SerializeField] GameObject player;
    [SerializeField] GameObject goal;

    // AI difficulty
    private int randomNumberCheck;

    // Difficulty Options
    private float distanceFromGoal;

    // Move Gate
    private float gateMovementAmount; // Speed of movement
    private float moveDirection; // Keeps track of direction (1 for moving right, -1 for left)
    private float timer; // Timer to track the 3-second intervals

    private void Awake()
    {
        // Set the instance to this GameManager, so it can be accessed from other scripts
        gameManagerInstance = this;
    }

    private void Start()
    {
        // Calling the setup methods
        InitializeVars();
        SetGameSetupOptions();
    }

    // Initialize the movement settings
    private void InitializeVars()
    {
        gateMovementAmount = 9f;
        moveDirection = 1f;
        timer = 0f;
        goalsAiScored = 0;
    }

    // Set game options based on the player's choices
    private void SetGameSetupOptions()
    {
        if (GameSetup.gameSetupInstance.useGravity)
        {
            UseGravity();
        }
        else
        {
            DontUseGravity();
        }

        if (GameSetup.gameSetupInstance.difficultyEasy)
        {
            EasyDifficulty();
        }
        if (GameSetup.gameSetupInstance.difficultyMedium)
        {
            MediumDifficulty();
        }
        if (GameSetup.gameSetupInstance.difficultyHard)
        {
            HardDifficulty();
        }

        if (GameSetup.gameSetupInstance.useRandomDistance)
        {
            RandomDistance();
        }
        if (GameSetup.gameSetupInstance.playerPickedDistance)
        {
            PlayerPickedDistance();
        }

        if (GameSetup.gameSetupInstance.allowGateMovement)
        {
            StartCoroutine(MoveGateAutomatically());
            goal.transform.position = new Vector3(-14, 1, 0);
        }
    }

    // Difficulty options
    public void EasyDifficulty()
    {
        // In easy difficulty, AI has a 3 out of 10 chance of scoring
        for (int i = 0; i < 10; i++)
        {
            randomNumberCheck = Random.Range(0, 10);
            if (randomNumberCheck == 0 || randomNumberCheck == 1 || randomNumberCheck == 2)
            {
                goalsAiScored++;
            }
        }
    }

    public void MediumDifficulty()
    {
        // In medium difficulty, AI has a 6 out of 10 chance of scoring
        for (int i = 0; i < 10; i++)
        {
            randomNumberCheck = Random.Range(0, 10);
            if (randomNumberCheck == 0 || randomNumberCheck == 1 || randomNumberCheck == 2 || randomNumberCheck == 3 || randomNumberCheck == 4 || randomNumberCheck == 5)
            {
                goalsAiScored++;
            }
        }
    }

    public void HardDifficulty()
    {
        // In hard difficulty, AI has a 9 out of 10 chance of scoring
        for (int i = 0; i < 10; i++)
        {
            randomNumberCheck = Random.Range(0, 10);
            if (randomNumberCheck != 9)
            {
                goalsAiScored++;
            }
        }
    }

    // Distance options

    public void RandomDistance()
    {
        // Player picked distance between 10 and 30 units
        distanceFromGoal = Random.Range(10, 31);

        // Convert 3D world position to 2D screen position
        Vector3 distancePos = Camera.main.WorldToScreenPoint(new Vector3(0, 0, distanceFromGoal));

        // Set player position based on the calculated distance
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -distancePos.z);
    }

    public void PlayerPickedDistance()
    {
        // Player picked distance based on input text (from GameSetup)
        distanceFromGoal = float.Parse(GameSetup.gameSetupInstance.inputedDistanceText.text);

        // Ensure the distance is within a valid range (10 to 30)
        if (distanceFromGoal >= 30)
        {
            distanceFromGoal = 30;
        }
        if (distanceFromGoal <= 10)
        {
            distanceFromGoal = 10;
        }

        // Convert the 3D position to screen space and update the player position
        Vector3 distancePos = Camera.main.WorldToScreenPoint(new Vector3(0, 0, distanceFromGoal));
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -distancePos.z);
    }

    public void MakeDistanceTen()
    {
        // Set the distance to 10
        distanceFromGoal = 10;

        // Convert to screen space
        Vector3 distancePos = Camera.main.WorldToScreenPoint(new Vector3(0, 0, distanceFromGoal));
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -distancePos.z);
    }


    // Use Gravity Options

    public void UseGravity()
    {
        player.GetComponent<Rigidbody>().useGravity = true;
    }

    public void DontUseGravity()
    {
        player.GetComponent<Rigidbody>().useGravity = false;
    }

    // Move Gate Options
    IEnumerator MoveGateAutomatically()
    {
        while (true) // Keep looping the movement
        {
            // Move the goal horizontally based on direction
            goal.transform.Translate(gateMovementAmount * moveDirection * Time.deltaTime, 0, 0);

            // Wait until the next frame
            yield return null;

            // Track elapsed time and reverse direction every 3 seconds
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                moveDirection *= -1f; // Reverse direction
                timer = 0f; // Reset timer
            }
        }
    }
}