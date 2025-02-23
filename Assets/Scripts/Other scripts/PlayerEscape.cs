using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerEscape : MonoBehaviour
{
    [SerializeField] Button playerEscapedButton;    //player escape button
    [SerializeField] PlayerMovement playerMovement;  // Reference to movement script
    [SerializeField] ChaseEngine chaserMovement;  // Reference to nathan script
    [SerializeField] GameObject train;

    void Start()
    {
        //turn train off
        train.SetActive(false);

        //turn button off
        playerEscapedButton.gameObject.SetActive(false);

        //adding listner
        playerEscapedButton.onClick.AddListener(PlayerEscaped);
    }

    void FreezeGame()
    {
        // Freezes the game
        Time.timeScale = 0f; 
    }
    void PlayerEscaped()
    {
        //show the first scene
        SceneManager.LoadSceneAsync(0);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Train"))
        {
            // freeze game and both player and chaser
            FreezeGame();
            playerMovement.enabled = false;
            chaserMovement.enabled = false;

            //show the retry button
            playerEscapedButton.gameObject.SetActive(true);

            //show the mouse back
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
