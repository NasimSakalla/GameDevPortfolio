using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxPos : MonoBehaviour
{
    [SerializeField] Transform PlayerPos;

    void Update()
    {
        // sticking the image to the player and camera
        transform.position = new Vector3(PlayerPos.position.x, PlayerPos.position.y, PlayerPos.position.z);
    }
}
