using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Image staminaBar;
    public float maxStamina;
    
    void Update()
    {
        staminaBar.fillAmount = PlayerMovement.counter / maxStamina ;   
    }
}
