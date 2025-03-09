using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEngine : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private Animator animControl;

    private bool canJump;
    private float xDir, speed , collectedCoins;

    void Start()
    {
        InitializePlayer();
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleAttackInput();
        HandleAnimation();
    }

    private void InitializePlayer()
    {
        transform.position = new Vector2(2.5f, 0.5f); // Reset player position
        canJump = false;
        speed = 7;
    }
    private void HandleMovementInput()
    {
        xDir = Input.GetAxis("Horizontal"); // Gets input from -1 (left) to 1 (right)

        if (xDir != 0)
        {
            //make sure the player is facing the right my multplying the scale with Xdir
            transform.localScale = new Vector2(Mathf.Sign(xDir) * 0.4f , transform.localScale.y);
        }
        //move player using veloctiy
        playerRB.velocity = new Vector2(xDir * speed, playerRB.velocity.y);
    }
    private void HandleJumpInput()
    {
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            //make player jump
            playerRB.AddForce(Vector2.up * 350);
            //play animation
            animControl.SetBool("isJumping", true);
            //play sound effect
            audioSource.PlayOneShot(jumpSoundEffect);
        }
    }
    private void HandleAttackInput()
    {
        //shoot an arrow
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(arrowPrefab, transform.position, transform.rotation);
        }
        //shoot a volley of three arrows
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateMultipleArrows();
        }
    }
    private void CreateMultipleArrows()
    {
        //create a volley of three arrows
        float yPos = 0;
        for (int i = 0; i < 3; i++)
        {
            yPos += 0.2f;
            Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y + yPos), transform.rotation);
        }
    }
    private void HandleAnimation()
    {
        //as long as player is holding down a movement input move him
        animControl.SetBool("isRunning", xDir != 0);
        //a sliding animation
        animControl.SetBool("IsSliding", Input.GetKey(KeyCode.N));
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