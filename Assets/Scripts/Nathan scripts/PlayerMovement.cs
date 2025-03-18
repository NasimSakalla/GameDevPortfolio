using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // rotation

    float mouseX, lookSpeed, mouseY , cameraRange;
    public Transform cameraTurn;

    // movement
    
    float moveSpeed , xAxis , zAxis;
    CharacterController CC;
    Vector3 localDir;

    //Gravity

    float radius , gravity;
    public LayerMask groundLayerMask;
    public Transform heightOfSphereAboveGround;
    public bool isGrounded;
    Vector3 gravityMove;
  
    void Start()
    {
        // rotation

        lookSpeed = 200;
        mouseX = 0;
        mouseY = 0;

        //movement

        CC = GetComponent<CharacterController>();
        moveSpeed = 50;

        //Gravity
        isGrounded = false;
        radius = 1f;
        gravity = -9.81f;

    }


    void Update()
    {
        Rotation();
        CCMove();
        GravityEngine();
    }

    void Rotation ()
    {
        //moving the Y rotaion of the player using the X movement of the mouse . (sides change Y rot)
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * lookSpeed;
        //rotates player
        transform.Rotate (0, mouseX, 0);
        //moving the X rotaion of the player using the Y movement of the mouse . (up and down change X rot)
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * lookSpeed;
        //flipping it so it works correctly otherwise up will look down and vice versa
        cameraRange -= mouseY;
        //setting boundries for my camera so the player cant do a 360 and look behind himself.
        cameraRange = Mathf.Clamp(cameraRange,-30f,30f);
        //turing the camera on the X axis using Euler's equations
        cameraTurn.localRotation = Quaternion.Euler(cameraRange, 0, 0);
    }
    void CCMove()
    {
        // store horiz input
        xAxis = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        // store vert input
        zAxis = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // add into our players local dir vertical movement(zAxis) and to affect horizontal movment (xAxis)
        localDir = transform.forward * zAxis + transform.right * xAxis;
        // using the func to "move" to move the player , very simlliar to velocity in 2d
        CC.Move(localDir);

        //crouch
        if (Input.GetKey(KeyCode.C))
        {
            transform.localScale = new Vector3 (1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1, 2, 1);
        }
    }
    void GravityEngine()
    {
        //this function spawns an invisible sphere on top of the sphere we already made using 
        //the "heightOfSphereAboveGround.position" and then the raidus is used and the center position
        //intersects with any collider , if so returns true.
        if (Physics.CheckSphere(heightOfSphereAboveGround.position,radius,groundLayerMask))
        {
           isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        // if not touching the ground activate gravity and pull the player downwards
        if (!isGrounded)
        {
            gravityMove.y += gravity * Time.deltaTime;
        }
        // if he is not in the air he isnt going to have any gravity on him , which is actually a flaw if for example
        // the player is falling off a ledge he would have gravity pull him and "isGrounded" would stay true therefore
        // he would get sucked down towards the floor really fast .
        else
        {
            gravityMove.y = 0;
        }
        // using the word "jump" written inside of unity everytime the spacebar is pressed , the command will activate 
        // making the player go in the air 10 units and making "isGrounded" into false until he touches the floor again.
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            gravityMove.y += 30;
        }
        // the line that actually excutes the code after we press the button using move func.( very simlliar to velocity) 
        CC.Move(gravityMove * Time.deltaTime);
    }
}
