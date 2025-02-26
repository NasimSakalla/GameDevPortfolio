using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] float distance;
    [SerializeField] float rotationSpeed;

    [SerializeField] float maxVerticalAngle;
    [SerializeField] float minVerticalAngle;

    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float rotationX;
    float rotationY;

    float invertXVal;
    float invertYVal;


    private void Start()
    {
        //mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //giving vars values
        distance = 5;
        rotationSpeed = 2;
        minVerticalAngle = -10;
        maxVerticalAngle = 45;



    }

    private void Update()
    {
        // checkbox yo invert y and x look
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        //normal set up of camera movement
        rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        
        rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        // moving caerma so its looking at our players chest/head.
        var foucsPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        //pos of camera behind player and we multply it by our targetRtoation is its looking at our player
        transform.position = foucsPosition - targetRotation * new Vector3(0,0,distance);
        // make camera have said roation
        transform.rotation = targetRotation; 
    }

    public Quaternion planarRotation => Quaternion.Euler(0,rotationY,0);

}
