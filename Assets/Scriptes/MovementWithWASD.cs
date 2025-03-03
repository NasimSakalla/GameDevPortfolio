using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovementWithWASD : MonoBehaviour
{
    // holds dir of player
    float directionX;
    float directionY;
    // to use bullet
    [SerializeField] GameObject PlayerProjectile;

    void Start()
    {
        directionX= 0;
        directionY= 0;
    }
    void Update()
    {
        IsOnScreen();
        UpdateMovementDirection();


        //moves player
        transform.Translate(directionX * 0.03f , directionY * 0.03f , 0);

        // fire projectile
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(PlayerProjectile, new Vector2(transform.position.x, transform.position.y + 1.5f), transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    { 
        // death when touching enemy ships
        if ((collision.gameObject.tag) == "Enemy")
        {
            Destroy(gameObject);

        }
    }
    void IsOnScreen()
    {
        if (transform.position.x > 9.5f || transform.position.x < -9.5f || transform.position.y < -6f || transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }
    
    void UpdateMovementDirection()
    {
        // Reset direction
        directionX = 0;
        directionY = 0;

        // Check for movement input
        if (Input.GetKey(KeyCode.D))
        {
            directionX = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            directionX = -1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            directionY = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            directionY = -1;
        }
    }

}
