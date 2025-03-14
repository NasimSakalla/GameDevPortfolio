using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPractice : MonoBehaviour
{
    public void StartPracticeFunction()
    {
        //load scene
        SceneManager.LoadSceneAsync(1);

        //turning off gravity
        GameObject.Find("player").GetComponent<Rigidbody>().useGravity = false;

        //setting distance
        GameManager.gameManagerInstance.MakeDistanceTen();
    }
}
