using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    //bool for detection
    public static bool isPlayerInRange;

    //we will use this in other scripts to lower the range of the detection , ex : player crouched
    private int detectionRange;

    //Transform of player used to check distance between them
    [SerializeField] Transform playerTransform;


    private void Update()
    {
        CheckDistanceWithPlayer();
        DecreaseDetectionRange();
    }

    void CheckDistanceWithPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }
    void DecreaseDetectionRange()
    {
        if (PlayerMovement.isCrouching)
        {
            detectionRange = 9;
        }
        if (!Flashlight.isFlashlightOn)
        {
            detectionRange = 11;
        }
        if (PlayerMovement.isCrouching && !Flashlight.isFlashlightOn)
        {
            detectionRange = 7;
        }
        if (!PlayerMovement.isCrouching && Flashlight.isFlashlightOn)
        {
            detectionRange = 13;
        }
    }

}
