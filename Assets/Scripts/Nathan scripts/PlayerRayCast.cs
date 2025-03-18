using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    Vector3 origin;     // originial pos of raycast
    RaycastHit infOfHit;     // saves info of hits

    float maxDistance;  // max range of ray
    public GameObject cameraPos; // need it to come from center of screen therefore it will use the camera to check for info
    public LayerMask rayCastHitable; // what not to hit


    void Start()
    {
        maxDistance = 500;
        Cursor.visible = false;                         // not showing cursor
        Cursor.lockState = CursorLockMode.Locked;       // keeping the cursor insdie the game window
    }


    void Update()
    {
        // left mouse button
        if (Input.GetMouseButton(0))
        {
            // viewportToWorldpoint = what the camera sees forward , vector 3 says where on
            // the screen it should take the info from , in our example the middle in (0.5,0.5,0).

            origin = cameraPos.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            // origin = start pos , cameraPos.transform.forward = direction , out infOfHit = where to store info  , rayCastHitable = the layers which the raycast dosent effect.
            if (Physics.Raycast(origin, cameraPos.transform.forward, out infOfHit, maxDistance, rayCastHitable))
            {
                // the object hit will get his meshrender turned into the color red
                infOfHit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
