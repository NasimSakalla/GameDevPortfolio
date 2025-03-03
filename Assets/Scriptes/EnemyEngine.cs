using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{
    float randomXpos , randomSpeedPositive, randomSpeedNegative , randomYpos;
    [SerializeField] Transform PlayerPos;

    void Start()
    {
        //setting random values of speed and position of each created spaceship
        PlayerPos = GameObject.Find("Spaceship player").transform;
        randomXpos = Random.Range(-9.5f, 10.5f);
        randomYpos = Random.Range(6f, 7f);
        randomSpeedPositive = Random.Range(1.5f, 2f);
        randomSpeedNegative = Random.Range(-2f, -1.5f);
        transform.position = new Vector2 (randomXpos , randomYpos);
    }

    // Update is called once per frame
    void Update()
    {
        //checking if left or right of player using the two funcs
        MovingEnemy(LeftOrRightOfPlayer());
        //cheecking if above or under player using the two funcs
        MovingEnemy(AboveOrUnderPlayer());
        //checking if enemy is still on screen
        IsOnScreen();
    }

    string LeftOrRightOfPlayer()
    {
        if (PlayerPos.position.x < transform.position.x)
        {
            return "Right";
        }
        else
        {
            return "Left";
        }
    }

    string AboveOrUnderPlayer()
    {
        if (PlayerPos.position.y < transform.position.y)
        {
            return "Above";
        }
        else
        {
            return "Under";
        }
    }

        
    void MovingEnemy(string dir)
    {
        if (dir == "Right")
        {
            transform.Translate(randomSpeedNegative * Time.deltaTime ,0,0);
        }
        if (dir == "Left")
        {
            transform.Translate(randomSpeedPositive * Time.deltaTime ,0,0);
        }
        if (dir == "Above")
        {
            transform.Translate(0, randomSpeedNegative * Time.deltaTime ,0);
        }
        if (dir == "Under")
        {
            transform.Translate(0 , randomSpeedPositive * Time.deltaTime, 0);
        }
    }

    void IsOnScreen()
    {
        if (transform.position.x > 12f || transform.position.x < -12f || transform.position.y < -6f)
        {
            Destroy(gameObject);
            ProjectileEngine.howManyDied++;
            PlayerScore.PlayerScoreValue += 100;
        }
    }
}
