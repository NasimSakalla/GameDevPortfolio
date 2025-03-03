using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = GameObject.Find("Snake").GetComponent<PlayerEngine>().amountEaten.ToString();
    }
}
