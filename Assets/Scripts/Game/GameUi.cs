using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText , scoreText , attemptsText;

    private void Update()
    {
        //Timer
        timerText.text = "Time : " + Mathf.RoundToInt(Time.timeSinceLevelLoad);
        //Attempts
        attemptsText.text = "Attempts : " + GameObject.Find("Game Manager").GetComponent<RaycastEngine>().attemptCounter;
        //Score
        scoreText.text = "Score : " + GameObject.Find("Game Manager").GetComponent<RaycastEngine>().scoreCounter;
        //Restting the game after the player found all the cards
        CheckIfPlayerWon();
    }

    void CheckIfPlayerWon()
    {
        if (GameObject.Find("Game Manager").GetComponent<RaycastEngine>().scoreCounter == 5)
        {
            StartCoroutine(EndGameDelay());
        }
    }

    IEnumerator EndGameDelay()
    {
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadSceneAsync(0);
    }
}
