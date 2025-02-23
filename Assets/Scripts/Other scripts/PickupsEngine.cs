using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupsEngine : MonoBehaviour
{
    public static int  Score;
    [SerializeField] GameObject train;

    private void Update()
    {
        PlayerPickedAllNotes();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Score++;
        }
    }
    void PlayerPickedAllNotes()
    {
        if (Score == 8)
        {
            //turn train on after player picks up all pappers
            train.SetActive(true);
        }
    }
}
