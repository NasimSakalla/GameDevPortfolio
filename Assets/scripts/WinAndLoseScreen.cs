using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinAndLoseScreen : MonoBehaviour
{
    [SerializeField] GameObject YouLose;
    [SerializeField] GameObject YouWin;
    [SerializeField] GameObject Ball;
    [SerializeField] Button restartButton;
    
    void Update()
    {
        PlayerLost();
        PlayerWon();
    }

    void PlayerLost()
    {
        if (ballEngine.CounterOfDeath == 3)
        {
            YouLose.SetActive(true);
            Ball.SetActive(false);
            restartButton.gameObject.SetActive(true);
        }
    }

    void PlayerWon()
    {
        if ( ballEngine.CounterOfBricks == 12)
        {
            YouWin.SetActive(true);
            Ball.SetActive(false);
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
