using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI CPUScoreText;
    [SerializeField] TextMeshProUGUI whoWonText;

    private void Start()
    {
        CheckWhoWon();   
    }
    void CheckWhoWon()
    {
        playerScoreText.text = $"The player score is : {BallManager.playerScore}";
        CPUScoreText.text = $"The CPU score is : {GameManager.gameManagerInstance.goalsAiScored}";


        //player won
        if (BallManager.playerScore > GameManager.gameManagerInstance.goalsAiScored)
        {
            whoWonText.text = "Player Won";
        }
        //CPU won
        if (BallManager.playerScore < GameManager.gameManagerInstance.goalsAiScored)
        {
            whoWonText.text = "CPU Won";
        }
        //A tie
        if (BallManager.playerScore == GameManager.gameManagerInstance.goalsAiScored)
        {
            whoWonText.text = "Nobody Won , Game tied";
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

