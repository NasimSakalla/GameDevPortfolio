using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements;

public class ArrowEngine : MonoBehaviour
{
    private float playerSign , speed;
    private Rigidbody2D arrowRB;
    [SerializeField] Transform PlayerScale;
    // Start is called before the first frame update
    void Awake()
    {
        // making sure the arrow and the player are facing the same dir
        PlayerScale = GameObject.Find("Player").transform;
        //get the rigidbody of this arrow
        arrowRB = transform.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        CheckPlayerDirection();
        FixArrowDirection();
    }

    private void Update()
    {
        IsArrowOffTheMap();
    }


    void CheckPlayerDirection()
    {
        // Check if the player is facing left or right by looking at the localScale
        playerSign = Mathf.Sign(PlayerScale.localScale.x);
    }
    
    void FixArrowDirection()
    {
        playerSign = Mathf.Sign(PlayerScale.localScale.x);
        speed = 30;
        // make the arrow face the same way as the player
        if (playerSign == -1)
        {
            //launch it
            arrowRB.velocity = new Vector2( speed * playerSign , arrowRB.velocity.y);
        }
        if (playerSign == 1)
        {
            //launch it
            arrowRB.velocity = new Vector2(speed * playerSign, arrowRB.velocity.y);
            //turn it around
            transform.localScale = new Vector2(-0.35f, transform.localScale.y);
        }
    }

    void IsArrowOffTheMap()
    {
        //if arrow is off the map destroy it
        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }
}

