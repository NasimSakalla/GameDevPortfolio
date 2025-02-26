using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float YdirKeys = 0;      // used to store keys input
    float XdirKeys = 0;      //

    float XposKeys = 0;      // used to control speed (with WASD)
    float YposKeys = 0;      //

    float XdirMouse = 0;     // used to store mouse input
    float YdirMouse = 0;     //

    float XposMouse = 0;     // used to control speed (with MOUSE)
    float YposMouse = 0;     //

    void Start()
    {
        XposKeys = -3.5f;
        YposKeys = 3;

        // Initialize Mouse Position to prevent jumps
        XposMouse = transform.position.x;
        YposMouse = transform.position.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Invoke(nameof(FixPos), 2);
    }

    void Update()
    {
        //KeysMovement();
        MouseMovement();
    }

    void KeysMovement()
    {
        XdirKeys = Input.GetAxis("Horizontal");
        YdirKeys = Input.GetAxis("Vertical");
        XposKeys += XdirKeys * 0.020f;
        YposKeys += YdirKeys * 0.020f;

        transform.position = new Vector2(XposKeys, YposKeys);
    }

    void MouseMovement()
    {
        XdirMouse = Input.GetAxis("Mouse X");
        YdirMouse = Input.GetAxis("Mouse Y");

        // Move relative to current position instead of resetting
        XposMouse += XdirMouse * 0.5f;
        YposMouse += YdirMouse * 0.5f;

        transform.position = new Vector2(XposMouse, YposMouse);
    }

    void FixPos()
    {
        transform.position = new Vector3(-4, 3, -1);
    }
}
