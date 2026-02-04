using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEngine : MonoBehaviour
{
    //raycast
    private int counter, rayDepth; 
    private RaycastHit hit;

    //rotation
    private bool allowRotation;

    //track clicks
    private GameObject firstCard, firstCardSymbol, firstCardOutline, secondCard, secondCardSymbol, 
    secondCardOutline, thirdCard , thirdCardSymbol, thirdCardOutline;
    //public so i can acsses them in GameUi
    public int attemptCounter , scoreCounter;
    private bool allowClick;

    private void Start()
    {
        //Setting depth of ray
        rayDepth = 10;

        //Zeroing values
        scoreCounter = 0;
        attemptCounter = 0;
        counter = 0;
        allowRotation = false;
        allowClick = true;
    }

    private void Update()
    {
        //Make sure the raycast is active every frame
        CreatingTheRaycast();
        //Checking if we should rotate the symbol
        AllowRotation();
        //Call upon certain functions after a click and when the colider isnt null
        AfterClicking();
    }

    //What to call after a succesful click
    void AfterClicking()
    {
        if (hit.collider != null && Input.GetMouseButtonDown(0))
        {
            MakeOutlineBlue();
            TrackClicks();
        }
    }


    //The raycast engine
    void CreatingTheRaycast()
    {
        // Convert the screen position to a world position with a fixed depth (e.g., 10 units away from the camera)
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, rayDepth));

        // Define a direction for the ray (e.g., from the camera to the mouse world position)
        Vector3 rayDirection = mouseWorldPosition - Camera.main.transform.position;

        // Visualize the ray in the Scene view
        Debug.DrawRay(Camera.main.transform.position, rayDirection, Color.cyan);

        //making the raycast
        Physics.Raycast(Camera.main.transform.position, rayDirection, out hit, rayDepth);
    }


    //A function used to show the symbol of the picked cards
    void ShowSymbol()
    {
        //turn off the visuals of the first card
        firstCard.GetComponent<MeshRenderer>().enabled = false;
        firstCardOutline.GetComponent<MeshRenderer>().enabled = false;
        //turn off the visuals of the second card
        secondCard.GetComponent<MeshRenderer>().enabled = false;
        secondCardOutline.GetComponent<MeshRenderer>().enabled = false;
        //turn off the visuals of the third card
        thirdCard.GetComponent<MeshRenderer>().enabled = false;
        thirdCardOutline.GetComponent<MeshRenderer>().enabled = false;

        //turning on the visuals of our symbols
        firstCardSymbol.transform.GetComponent<MeshRenderer>().enabled = true;
        secondCardSymbol.transform.GetComponent<MeshRenderer>().enabled = true;
        thirdCardSymbol.transform.GetComponent<MeshRenderer>().enabled = true;
    }


    //A function used to hide the symbol of the picked cards
    void HideSymbol()
    {
        //turn on the visuals of the first card
        firstCard.GetComponent<MeshRenderer>().enabled = true;
        firstCardOutline.GetComponent<MeshRenderer>().enabled = false;
        firstCardSymbol.GetComponent<MeshRenderer>().enabled = false;
        //turn on the visuals of the second card
        secondCard.GetComponent<MeshRenderer>().enabled = true;
        secondCardOutline.GetComponent<MeshRenderer>().enabled = false;
        secondCardSymbol.GetComponent<MeshRenderer>().enabled = false;
        //turn on the visuals of the third card
        thirdCard.GetComponent<MeshRenderer>().enabled = true;
        thirdCardOutline.GetComponent<MeshRenderer>().enabled = false;
        thirdCardSymbol.GetComponent<MeshRenderer>().enabled= false;
        //Add one attempt to the counter
        attemptCounter++;
    }


    //Activates the blue out line of a card
    void MakeOutlineBlue()
    {
        if (allowClick)
        {
            //gets child who is outline blue and activates him
            hit.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    
    //Allows the rotation the symbols of our cards for four seconds then stops
    void AllowRotationOfSymbol()
    {
        //we call this function later , after the third click
        allowRotation = true;
        StartCoroutine(StopRotation());
    }


    //Rotates our symbols
    void RotateSymbol()
    {
        //A simple rotation function
        if (firstCardSymbol != null && secondCardSymbol != null && thirdCardSymbol != null)
        {
            firstCardSymbol.transform.Rotate(0, 7, 0);
            secondCardSymbol.transform.Rotate(0, 7, 0);
            thirdCardSymbol.transform.Rotate(0, 7, 0);
        }
    }


    //Used to check the change of the bool and cleaner code
    void AllowRotation()
    {
        if (allowRotation)
        {
            RotateSymbol();
        }
    }


    //A function that keeps track of the players clicks 
    void TrackClicks()
    {
        if (allowClick)
        {
            if (counter == 0)
            {
                //Get first clicked card
                firstCard = hit.collider.gameObject;
                //Get first clicked card shape
                firstCardSymbol = hit.transform.GetChild(0).gameObject;
                //Get first clicked card outline
                firstCardOutline = hit.transform.GetChild(1).gameObject;
                //Stop the player from clicking on the same card
                firstCard.GetComponent<MeshCollider>().enabled = false;
            }
            if (counter == 1)
            {
                //Get second clicked card
                secondCard = hit.collider.gameObject;
                //Get second clicked card shape
                secondCardSymbol = hit.transform.GetChild(0).gameObject;
                //Get second clicked card outline
                secondCardOutline = hit.transform.GetChild(1).gameObject;
                //Stop the player from clicking on the same card
                secondCard.GetComponent<MeshCollider>().enabled = false;
            }
            if (counter == 2)
            {
                //Get third clicked card
                thirdCard = hit.collider.gameObject;
                //Get third clicked card shape
                thirdCardSymbol = hit.transform.GetChild(0).gameObject;
                //Get third clicked card outline
                thirdCardOutline = hit.transform.GetChild(1).gameObject;
                //Stop the player from clicking on the same card
                thirdCard.GetComponent<MeshCollider>().enabled = false;
            }
            counter++;

            if (counter == 3)
            {
                //Stopping player from clicking on targets until they stop spinning
                allowClick = false;
                //Function is called to turn off mesh renders and show the symbol 
                ShowSymbol();
                //Function is called to spin the symbol
                AllowRotationOfSymbol();

                //Check if player choose the same shapes
                if (firstCardSymbol.name == secondCardSymbol.name && secondCardSymbol.name == thirdCardSymbol.name)
                {
                    //If player picked correctly destroy cards and give him points
                    Destroy(firstCard, 4);
                    Destroy(secondCard, 4);
                    Destroy(thirdCard, 4);
                    //give player points
                    scoreCounter++;
                    //allow clicks after four seconds
                    StartCoroutine(AllowClickCooldown());
                }
                //player was wrong , reset cards
                else
                {
                    StartCoroutine(ShowSymbolCooldown());
                }

                //reset counter
                counter = 0;

                //Allow cards to be clicked on again 
                firstCard.GetComponent<MeshCollider>().enabled = true;
                secondCard.GetComponent<MeshCollider>().enabled = true;
                thirdCard.GetComponent<MeshCollider>().enabled = true;
            }
        }   
    }

    //-----------------------------------------------------------------------------------------------------------------------------------
    //IEnumerators below

    IEnumerator StopRotation()
    {
        //A four second delay
        yield return new WaitForSecondsRealtime(4);
        allowRotation = false;
        
    }
    IEnumerator ShowSymbolCooldown()
    {
        //A four second delay
        yield return new WaitForSecondsRealtime(4);
        //After four seconds hide the symbol and allow clicks
        allowClick = true;
        HideSymbol();
    }
    IEnumerator AllowClickCooldown()
    {
        //After four seconds allow clicks
        yield return new WaitForSecondsRealtime(4);
        allowClick = true;
    }

}
