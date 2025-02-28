using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballEngine : MonoBehaviour
{

    public Rigidbody2D ballForce;
    public bool isForce;
    public static bool hitBorder;
    public static int CounterOfDeath;
    public static int CounterOfBricks;

    void Start()
    {
        CounterOfBricks = 0;
        CounterOfDeath = 0;
        isForce = true;
        hitBorder = false;

    }


    void Update()
    {
        LaunchBall();


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && !hitBorder)
        {
            CounterOfDeath++;
            gameObject.SetActive(false);
            transform.position = new Vector3(0, -19, -1);
            hitBorder = true;
            isForce= true;
        }

        gameObject.SetActive(true);
        hitBorder = false;

        if (collision.gameObject.tag == "Brick")
        {
            Destroy(collision.gameObject);
            CounterOfBricks++;

        }
        
    }

    void LaunchBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && isForce == true)
        {
            ballForce.AddForce(new Vector2(0,800));
            isForce = false;
        }
    }

}
