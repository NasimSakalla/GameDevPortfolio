using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    // Making an instance of this script to access these bool's easily
    public static GameSetup gameSetupInstance;

    // Booleans to send depending on button clicks
    public bool allowGateMovement, useGravity, difficultyEasy, difficultyMedium, difficultyHard, useRandomDistance, playerPickedDistance;

    // Inputted distance
    public TMP_InputField inputedDistanceText;

    // Setting up the toggles so I can access the IsOn component
    private Toggle gateMovementToggle;
    private Toggle useGravityToggle;
    private Toggle useRandomDistanceToggle;

    private void Awake()
    {
        gameSetupInstance = this;

        // Find the toggles by name
        gateMovementToggle = GameObject.Find("Gate Movement Toggle").GetComponent<Toggle>();
        useGravityToggle = GameObject.Find("Gravity Toggle").GetComponent<Toggle>();
        useRandomDistanceToggle = GameObject.Find("Random Position Toggle").GetComponent<Toggle>();
    }

    private void Start()
    {
        InitializeSettings();
    }

    private void Update()
    {
        // Update the booleans based on toggle values
        UpdateToggleSettings();

        // Ensure the player picked position is valid
        PlayerPickedPosition();
    }

    // Initialize default settings
    private void InitializeSettings()
    {
        allowGateMovement = false;
        useGravity = true;
        difficultyEasy = true;
        difficultyMedium = false;
        difficultyHard = false;
        useRandomDistance = false;
        playerPickedDistance = true;
    }

    // Update the toggle settings based on their current state
    private void UpdateToggleSettings()
    {
        allowGateMovement = gateMovementToggle.isOn;
        useGravity = useGravityToggle.isOn;
        useRandomDistance = useRandomDistanceToggle.isOn;
    }

    // Difficulty selection methods
    public void DifficultyEasy()
    {
        difficultyEasy = true;
        difficultyMedium = false;
        difficultyHard = false;
    }

    public void DifficultyMedium()
    {
        difficultyMedium = true;
        difficultyEasy = false;
        difficultyHard = false;
    }

    public void DifficultyHard()
    {
        difficultyHard = true;
        difficultyEasy = false;
        difficultyMedium = false;
    }

    // Random distance setting
    public void UseRandomDistance()
    {
        playerPickedDistance = false;
        useRandomDistance = true;
    }

    // Player picked distance setting
    public void PlayerPickedPosition()
    {
        if (!useRandomDistance)
        {
            // Try to parse the input as a float
            if (float.TryParse(inputedDistanceText.text, out float _))
            {
                playerPickedDistance = true;
            }
            else
            {
                Debug.Log("The string contains non-numeric characters, please write only numbers.");
            }
        }
    }
}
