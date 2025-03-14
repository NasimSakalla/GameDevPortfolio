using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPracticeForTest : MonoBehaviour
{
    public bool gravIsOff;
    public GameObject player;
    public StartPracticeForTest grav;

    private void Awake()
    {
        grav = this;
    }
    public void StartPracticeFunction()
    {
        //turning off gravity
        player.GetComponent<Rigidbody>().useGravity = false;   
        gravIsOff = true;
    }
}
