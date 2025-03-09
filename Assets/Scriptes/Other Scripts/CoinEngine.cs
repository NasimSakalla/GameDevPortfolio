using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEngine : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;  // Reference to the single coin prefab
    [SerializeField] private Vector2[] spawnPoints; // Array to hold spawn points

    private GameObject[] instantiatedCoins; // Array to hold instantiated coins
    private Color[] colors = new Color[] { Color.blue, Color.red, Color.yellow, Color.black, Color.white };

    private void Start()
    {
        InitializonFunction();
    }

    private void Update()
    {
        RotateCoins();
    }
    private void InitializonFunction()
    {
        instantiatedCoins = new GameObject[spawnPoints.Length]; // Initialize the instantiatedCoins array
        CreateCoins();
    }

    // Creates coins at the spawn points with random colors
    void CreateCoins()
    {
        // Loop through the spawn points and instantiate coins at those positions
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Instantiate each coin at the spawn point position
            instantiatedCoins[i] = Instantiate(coinPrefab, spawnPoints[i], transform.rotation);

            // Set the instantiated coin's parent to be this object
            instantiatedCoins[i].transform.SetParent(transform);

            // Assign a random color to the coin's sprite renderer
            int randomColor = Random.Range(0, colors.Length);
            instantiatedCoins[i].GetComponent<SpriteRenderer>().color = colors[randomColor];
        }
    }

    // Rotates the coins
    void RotateCoins()
    {
        // Loop through each instantiated coin to apply rotation
        foreach (GameObject coin in instantiatedCoins)
        {
            if (coin != null)
            {
                coin.transform.Rotate(0, 300 * Time.deltaTime, 0);  // Rotate each coin on the Y-axis
            }
        }
    }
}
