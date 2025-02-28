using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerAndPoints : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public static int score;
    [SerializeField] TextMeshProUGUI timerText;
    private int timer;


    private void Start()
    {
        //timer
        timer = 30;
        InvokeRepeating(nameof(Timer) , 0.1f ,1);

        //points
        scoreText.text = "0";
        score = 0;

    }

    void Timer()
    {
        if (timer == 1)
        {
            SceneManager.LoadSceneAsync(0);
            CancelInvoke();
        }

        timer--;
        timerText.text = timer.ToString();
    }
    public void Points()
    {
        scoreText.text = score.ToString();
    }

    
      
}
