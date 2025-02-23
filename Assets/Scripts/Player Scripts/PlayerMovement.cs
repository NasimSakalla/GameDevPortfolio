using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //audio

    [SerializeField] AudioSource audioSourceForWalk;
    [SerializeField] AudioSource audioSourceForForest;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip forestSound;

    // rotation

    float mouseX, lookSpeed, mouseY, cameraRangeY , cameraRangeX;
    [SerializeField] Transform cameraTurn;

    // movement

    bool isSprinting , allowSprint ;
    public static float counter;
    public static bool isCrouching;
    float moveSpeed, xAxis, zAxis;
    CharacterController CC;
    Vector3 localDir;

    //Gravity

    float radius, gravity;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform heightOfSphereAboveGround;
    bool isGrounded;
    Vector3 gravityMove;

    void Start()
    {
        //audio
        audioSourceForForest.PlayOneShot(forestSound);

        //rotation
        lookSpeed = 130;
        mouseX = 0;
        mouseY = 0;

        //movement
        CC = GetComponent<CharacterController>();
        moveSpeed = 5;
        counter = 0;
        allowSprint = true;
        isSprinting = false;
        isCrouching = false;

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
        Audio();
    }

    void Rotation()
    {
        //moving the Y rotaion of the player using the X movement of the mouse . (sides change Y rot)
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * lookSpeed;
        transform.Rotate(0,mouseX,0);
        cameraRangeX += mouseX;
        //moving the X rotaion of the player using the Y movement of the mouse . (up and down change X rot)
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * lookSpeed;
        //flipping it so it works correctly otherwise up will look down and vice versa and also accumlate it , very improtant!
        cameraRangeY -= mouseY;
        //setting boundries for my camera so the player cant do a 360 and look behind himself.
        cameraRangeY = Mathf.Clamp(cameraRangeY, -30f, 30f);
        //turing the camera on the X axis using Euler's equations
        cameraTurn.localRotation = Quaternion.Euler(cameraRangeY, cameraRangeX, 0);
    }
    void CCMove()
    {
        // store horiz input
        xAxis = Input.GetAxis("Horizontal");
        // store vert input
        zAxis = Input.GetAxis("Vertical");
        // add into our players local dir vertical movement(zAxis) and to affect horizontal movment (xAxis)
        localDir = transform.forward * zAxis + transform.right * xAxis;
        // using the func to "move" to move the player , very simlliar to velocity in 2d
        // Check if localDir has non-zero length before normalizing
        if (localDir.sqrMagnitude > 0) // This is more efficient than checking against Vector3.zero
        {
            localDir.Normalize(); // Normalize the direction for consistent speed
        }
        CC.Move(moveSpeed * Time.deltaTime * localDir); // Use the (possibly normalized) direction to move the player

        //crouch
        if (Input.GetKey(KeyCode.C))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            isCrouching = true;
            moveSpeed = 3;
        }
        else
        {
            transform.localScale = new Vector3(1, 1 , 1);
            isCrouching = false;
            moveSpeed = 5;
        }

        // Sprint
        if (allowSprint && !isCrouching)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 10;  // Increase speed for sprint
                if (counter <= 35)  // Only increment the counter if it's under 35
                {
                    counter += 0.1f; // Increment sprint time
                }
            }
        }

        if (counter > 35 || Input.GetKeyUp(KeyCode.LeftShift))  // Stop sprint if over time or key is released
        {
            moveSpeed = 5;     // Reset to normal speed
            if (!isSprinting)  // Start coroutine only if it’s not already running
            {
                StartCoroutine(StopSprint());
            }
        }

        // Sprint stop coroutine
        IEnumerator StopSprint()
        {
            allowSprint = false;
            isSprinting = true;                     // Ensure we don’t start this coroutine again
            yield return new WaitForSeconds(7);     // Wait for cool-down
            counter = 0;                            // Reset counter for next sprint
            isSprinting = false;                    // Allow sprinting again
            allowSprint = true;
        }
    }
    void GravityEngine()
    {
        //this function spawns an invisible sphere on top of the sphere we already made using 
        //the "heightOfSphereAboveGround.position" and then the raidus is used and the center position
        //intersects with any collider , if so returns true.
        if (Physics.CheckSphere(heightOfSphereAboveGround.position, radius, groundLayerMask))
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
            gravityMove.y += 3.5f;
        }
        // the line that actually excutes the code after we press the button using move func.( very simlliar to velocity) 
        CC.Move(gravityMove * Time.deltaTime);


    }
    void Audio()
    {
        //magnitude check if the vector any size or lenght not regarding its direction
        if (localDir.magnitude > 0 && !audioSourceForWalk.isPlaying)
        {
            // Play walking sound 
            audioSourceForWalk.PlayOneShot(walkSound);
        }
        //stop walk sound
        if (localDir.magnitude <= 0 && audioSourceForWalk.isPlaying)
        {
            audioSourceForWalk.Stop();
        }
    }
}


