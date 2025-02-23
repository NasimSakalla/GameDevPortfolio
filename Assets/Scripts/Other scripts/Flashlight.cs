using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light flashlight;
    [SerializeField] AudioSource flashlightSource;
    [SerializeField] AudioClip turnOn;
    [SerializeField] AudioClip turnOff;

    public static bool isFlashlightOn;

    void Start()
    {
        isFlashlightOn = false;
        flashlight.intensity = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFlashlightOn)
            {
                flashlightSource.PlayOneShot(turnOff);
                flashlight.intensity = 0;
            }
            else
            {
                flashlightSource.PlayOneShot(turnOn);
                flashlight.intensity = 0.75f;
            }

            isFlashlightOn = !isFlashlightOn;  // Toggle the flashlight state
        }
    }
}
