using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameFunction()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
