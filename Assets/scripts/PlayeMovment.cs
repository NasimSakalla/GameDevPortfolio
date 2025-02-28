using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    float xPos, dirX;
    public float speed;

    void Start()
    {
        speed = 15f;  
        transform.position = new Vector3(0, -25,-1);
        xPos = 0;


    }

    // Update is called once per frame
    void Update()
    {
        // dirX =  Input.GetAxis("Horizontal");
        // xPos = xPos + dirX * speed;        

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 32f)
        {
            xPos = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -32f)
        {
            xPos = -1;
        }
        else
        {
            xPos = 0;
        }

        transform.position = new Vector3(transform.position.x + xPos * speed * Time.deltaTime, -25f , -1);
        //transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal"), transform.position.y + Input.GetAxis("Vertical") , -1);

    }






}

