using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SuperManEngine : MonoBehaviour
{
    private float flyingSpeed , zAxis , xAxis , forwardForce , movementSpeed;
    [SerializeField] TextMeshProUGUI scoreTMP;


    private void Start()
    {
        //adding rb to superman and turning off his gravity
        gameObject.AddComponent<Rigidbody>().useGravity = false;

        //giving our floats a value
        forwardForce = 20;
        flyingSpeed = 15;
        movementSpeed = 350;
        zAxis = 0;
        xAxis = 0;

        //start value zero so he wont fly off
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //locking mouse crusor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        Movement();
        ShowPlayerScore();
    }

    void ShowPlayerScore()
    {
        scoreTMP.text = SpawningHumanEngine.scoreOfPlayer.ToString();
    }


    void Movement()
    {
        //setting up that superman movement and mkaing sure he always flys forward 

        //right and left
        xAxis = Input.GetAxis("Mouse X") * Time.deltaTime * movementSpeed;
        //up and down
        zAxis = Input.GetAxis("Mouse Y") * Time.deltaTime * movementSpeed;
        //always fly forward
        forwardForce = transform.up.z * flyingSpeed;
        //putting them into a a vector so it looks nicer
        Vector3 dir = new Vector3 (xAxis, zAxis , forwardForce);


        //iniate movement using velocity
        gameObject.GetComponent<Rigidbody>().velocity = dir;

        //freeze rotation so when he touches the floor he dosent go all over the place
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;

    }
}
