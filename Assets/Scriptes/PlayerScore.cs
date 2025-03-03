using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static int PlayerScoreValue = 0;
    private TextMeshProUGUI Score;
    // Start is called before the first frame update
    void Awake()
    {
        Score = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score :" + PlayerScoreValue;
    }
}
