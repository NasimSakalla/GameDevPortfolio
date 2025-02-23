using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRelated : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;                         // not showing cursor
        Cursor.lockState = CursorLockMode.Locked;       // keeping the cursor insdie the game window 
    }
}
