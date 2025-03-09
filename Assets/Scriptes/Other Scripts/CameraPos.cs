using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField] Transform PlayerPos;

    void Update()
    {   // making the camera follow the player
        transform.position = new Vector3(PlayerPos.position.x,PlayerPos.position.y , -10);
    }
}
