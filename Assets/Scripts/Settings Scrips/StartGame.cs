using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
     public void StartGameFunc()
     {
        //Load scene
        SceneManager.LoadSceneAsync(1);
     } 
}
