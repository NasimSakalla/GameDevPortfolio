using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraEngine : MonoBehaviour
{
    //we have this object on our player , our camera uses his position instead of being a child of the player
    //this way when he crouches the scale of the flashlight wont change
    [SerializeField] Transform OBJofPos;

    void Update()
    {
        transform.position = OBJofPos.position;
    }
}
