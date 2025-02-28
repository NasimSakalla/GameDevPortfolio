using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MouseRaycast : MonoBehaviour
{
    private RaycastHit2D hit;

    //pickups
    private int pickUpValue;
    private bool isDoubleOn;

    //timer
    private int timer;
    private bool allowTimeToMove;


    private void Start()
    {
        pickUpValue = 0;
        isDoubleOn = false;
        timer = 0;
        allowTimeToMove = false;

    }
    private void Update()
    {
        MakeRay();
        RayCollision();
        Timer();
    }

    void Timer()
    {
        if (allowTimeToMove)
        {
            timer++;

            if (timer == 4)
            {
                isDoubleOn = false;
                allowTimeToMove = false;
                timer = 0;
            }

            StartCoroutine(WaitOneSecond());
        }
    }
    void MakeRay()
    {
        //origin of ray , the screen point of my mouse
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //making the ray and storing the info in hit
        hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
    }
    void RayCollision()
    {
        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            if (hit.collider.CompareTag("Bomb Pickup"))
            {
                pickUpValue = 5;
                if(isDoubleOn)
                {
                    pickUpValue = 10;
                }
                TimerAndPoints.score -= pickUpValue;
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.CompareTag("Times Two Pickup"))
            {
                isDoubleOn = true;
                Destroy(hit.collider.gameObject);

                allowTimeToMove = true;
            }
            if (hit.collider.CompareTag("Plus One Pickup"))
            {
                pickUpValue = 1;
                if (isDoubleOn)
                {
                    pickUpValue = 2;
                }
                TimerAndPoints.score += pickUpValue;
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.CompareTag("Plus Three Pickup"))
            {
                pickUpValue = 3;
                if (isDoubleOn)
                {
                    pickUpValue = 6;
                }
                TimerAndPoints.score += pickUpValue;
                Destroy(hit.collider.gameObject);
            }

            ////dont go under zero , i changed this so points can go under zero
            //if (TimerAndPoints.score <= 0)
            //{
            //    TimerAndPoints.score = 0;
            //}

            //update text points
            GameObject.Find("Game Manager").GetComponent<TimerAndPoints>().Points();
        }
    }
    IEnumerator WaitOneSecond()
    {
        allowTimeToMove= false;
        yield return new WaitForSecondsRealtime(1);
        allowTimeToMove = true;
    }

}
