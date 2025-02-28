using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameFive()
    {
        SceneManager.LoadSceneAsync(1);

        BlockSpawner.spawnCount = 5;
    }
}
