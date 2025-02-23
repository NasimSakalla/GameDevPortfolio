using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button showInstructionButton;
    public Button startGameButton;
    public GameObject startImage;
    public GameObject instructionImage;

    private void Start()
    {
        instructionImage.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);

        showInstructionButton.onClick.AddListener(ShowInstruction);
        startGameButton.onClick.AddListener(StartingTheGame);
    }

    void ShowInstruction()
    {
        showInstructionButton.gameObject.SetActive(false);
        startImage.SetActive(false);
        startGameButton.gameObject.SetActive(true);
        instructionImage.SetActive(true);
    }

    void StartingTheGame()
    {
        SceneManager.LoadSceneAsync(1);
    }



}
