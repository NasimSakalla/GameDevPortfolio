using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEngineWithButtons : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private Animator animControl;

    private bool canJump;
    private float xDir, speed, collectedCoins;

    private void Start()
    {
        InitializePlayer();
    }
    private void Update()
    {
        HandleMovementInput();
        HandleAnimation();
    }
    private void InitializePlayer()
    {
        transform.position = new Vector2(2.5f, 0.5f); // Reset player position
        canJump = false;
        speed = 7;
    }
    public void HandleButtonInput(string button)
    {
        //check cases with buttons
        switch (button)
        {
            case "FIRE":
                HandleAttackInput();
                break;

            case "JUMP":
                HandleJumpInput();
                break;

            case "RIGHT":
                MoveRight();
                break;

            case "LEFT":
                MoveLeft();
                break;

            case "STOP":
                StopMovement();
                break;
        }
    }
    private void HandleJumpInput()
    {
        if (canJump)
        {
            //make player jump
            playerRB.AddForce(Vector2.up * 350);
            //play animation
            animControl.SetBool("isJumping", true);
            //play sound effect
            audioSource.PlayOneShot(jumpSoundEffect);
        }
    }
    private void MoveRight()
    {
        xDir = 1;
        transform.localScale = new Vector2(0.4f, transform.localScale.y); // Flip the player sprite to face right
    }
    private void MoveLeft()
    {
        xDir = -1;
        transform.localScale = new Vector2(-0.4f, transform.localScale.y); // Flip the player sprite to face left
    }
    private void StopMovement()
    {
        xDir = 0;
    }
    private void HandleMovementInput()
    {
        playerRB.velocity = new Vector2(speed * xDir, playerRB.velocity.y);
    }
    private void HandleAttackInput()
    {
        //shoot an arrow
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(arrowPrefab, transform.position, transform.rotation);
        }
    }
    private void HandleAnimation()
    {
        //as long as player is holding down a movement input move him
        animControl.SetBool("isRunning", xDir != 0);
    }
    private void HandleWinCondition()
    {
        if (collectedCoins == 7)
        {
            SceneManager.LoadSceneAsync(3);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnCollisionStay2D(Collision2D collision)
    {
        //player is ob the ground , so player is allowed to jump again
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            animControl.SetBool("isJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //player is off the ground , so player isnt allowed to jump again
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player picked up coins
        if (collision.gameObject.CompareTag("Coin"))
        {
            collectedCoins++;
            Destroy(collision.gameObject);
            HandleWinCondition();
        }
    }
}