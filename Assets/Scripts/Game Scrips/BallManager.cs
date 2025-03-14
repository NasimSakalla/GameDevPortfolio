using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public bool didPlayerKick;
    private int playerKicks;
    private Vector3 initialPlayerPos;

    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button kickButton;

    public static int playerScore;

    private void Start()
    {
        Invoke(nameof(GetPlayerInitialPos), 0.1f);
        //InvokeRepeating(nameof(DidPlayerKickUpdate), 0f, 0.01f); // Runs every 1 sec
        InitializeVars();
    }
    private void Update()
    {
        DidPlayerKickUpdate();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Initialize variables
    private void InitializeVars()
    {
        playerScore = 0;
        playerKicks = 0;
        didPlayerKick = false;
        UpdateScore();
    }

    // Update the player score UI
    private void UpdateScore()
    {
        scoreText.text = $"{playerScore}/10"; // Cleaner formatting
    }

    // Get the initial position of the ball after a short delay
    private void GetPlayerInitialPos()
    {
        initialPlayerPos = transform.position;
    }

    // Check if the player has kicked and handle logic
    private void DidPlayerKickUpdate()
    {
        if (didPlayerKick)
        {
            Invoke(nameof(PlayerMissed), 5);
            kickButton.interactable = false; // Stop the button from being interactble until the ball is reset
            didPlayerKick = false; // Reset the bool to ensure it only runs once
        }
    }

    // Check if the player has finished shooting
    private void IsPlayerDoneShooting()
    {
        if (playerKicks >= 10)
        {
            RestartGame();
        }
    }

    // Called when the player misses both the goal and the keeper
    private void PlayerMissed()
    {
        ResetBall();
        IsPlayerDoneShooting();
        playerKicks++;
    }

    // Reset the ball position and stop movement
    private void ResetBall()
    {
        playerRB.velocity = Vector3.zero;  // Stop player movement
        transform.position = initialPlayerPos;  // Reset position to the initial one
        didPlayerKick = false;  // Reset the kick flag
        kickButton.interactable = true;  // Enable button interaction after reset
        CancelInvoke(nameof(PlayerMissed));  // Cancel any pending "PlayerMissed" invoke
        UpdateScore();  // Update score (if needed)
    }

    //Restart the game after a delay
    private void RestartGame()
    {
        // Start the coroutine with a 2-second delay before restarting the game
        StartCoroutine(RestartGameDelay(1f));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            playerScore++;
            playerKicks++;
            CancelInvoke(nameof(PlayerMissed)); // Cancels the missed invoke if scored
            ResetBall();
            IsPlayerDoneShooting();
        }
        else if (collision.gameObject.CompareTag("Goal Keeper"))
        {
            playerKicks++;
            CancelInvoke(nameof(PlayerMissed)); // Cancels the missed invoke if blocked
            ResetBall();
            IsPlayerDoneShooting();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Coroutine to restart the game after a delay
    private IEnumerator RestartGameDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay
        SceneManager.LoadSceneAsync(2);  // Load the first scene (usually the main menu or starting scene)
    }
}
