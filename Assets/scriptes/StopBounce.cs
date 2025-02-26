using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StopBounce : MonoBehaviour
{
    [SerializeField] CircleCollider2D ballCC;
    [SerializeField] TextMeshProUGUI pointsText;
    private int points;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RIM"))
        {
            transform.position = new Vector3(3,2,-1);
            points++;
        }
    }

    private void Update()
    {
        pointsText.text = points.ToString();
    }
}