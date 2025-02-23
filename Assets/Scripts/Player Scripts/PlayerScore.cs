using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    //a simple code for making a score count

    public TextMeshProUGUI scoreText;
    private void Update()
    {
        ScoreOfPlayer();
    }

    // score

    void ScoreOfPlayer()
    {
        scoreText.text = "Test Answers Found " + PickupsEngine.Score + "/8";
    }
}
