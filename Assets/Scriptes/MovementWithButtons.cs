using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovementWithButtons : MonoBehaviour
{
    // to use velocity
    [SerializeField] Rigidbody2D PlayerMovement;
    // holds dir of player
    float directionX;
    float directionY;
    // to use bullet
    [SerializeField] GameObject PlayerProjectile;
    bool isFire;
    void Start()
    {
        directionY = 0;
        directionX = 0;
        isFire = false;

    }
    void Update()
    {
        //moves player using velocity
        PlayerMovement.velocity = new Vector2(directionX * 8, directionY * 5);


        if (isFire == true)
        {
            Instantiate(PlayerProjectile, new Vector2(transform.position.x, transform.position.y + 1.5f), transform.rotation);
            isFire = false;
        }

    }

    public void PlayingWithButtons(string input)
    {
        if (input == "R")
        {
            directionX = 1;
            directionY = 0;
        }
        if (input == "L")
        {
            directionX = -1;
            directionY = 0;
        }
        if (input == "U")
        {
            directionX = 0;
            directionY = 1;
        }
        if (input == "D")
        {
            directionX = 0;
            directionY = -1;
        }
        if (input == "STOP")
        {
            directionX = 0;
            directionY = 0;
        }

        if (input == " ")
        {
            isFire = true;
        }
    }


}
