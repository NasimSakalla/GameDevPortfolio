using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector3 groundCheckOffset;

    Quaternion targetRotation;

    bool isGrounded;
    float ySpeed;

    CameraController cameraController;
    CharacterController CC;
    Animator animator;

    private void Start()
    {
        groundCheckRadius = 0.2f;
        moveSpeed = 5f;
        rotationSpeed = 300f;
    }

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        CC = GetComponent<CharacterController>();   
    }

    private void Update()
    {

        GroundCheck();

        //movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //checking if player is moving
        float moveAmount = Mathf.Clamp01( Mathf.Abs(h) + Mathf.Abs(v));

        var moveInput = new Vector3(h, 0, v).normalized;
        // make player move in regardes to camera roataion
        var moveDir = cameraController.planarRotation * moveInput;

        moveDir.y = 0;


        if (isGrounded)
        {
            ySpeed = -0.5f;
        }
        else 
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
        
        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;


        CC.Move(moveDir * moveSpeed * Time.deltaTime);

        if (moveAmount > 0)
        {
            // make player face in regardes to camera roataion
            targetRotation = Quaternion.LookRotation(moveDir);
        }
        //roatate player slowly instead of it being instante 
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount" , moveAmount , 0.2f ,Time.deltaTime);
    }

    void GroundCheck()
    {
       isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset),
            groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Use a solid color for visibility
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }



}
