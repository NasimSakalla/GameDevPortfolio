using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem; 

public class AnimateAndInput : MonoBehaviour
{
    //reads click value from our vr controller
    [SerializeField]InputActionProperty pinchAnimAction;
    [SerializeField]InputActionProperty gripAnimAction;

    [SerializeField] float triggerValue;
    [SerializeField] float gripValue;

    [SerializeField] Animator handAnim;

    void Update()
    {
        //pich anim
        triggerValue = pinchAnimAction.action.ReadValue<float>();
        handAnim.SetFloat("Trigger" , triggerValue);
        //grip anim
        gripValue = gripAnimAction.action.ReadValue<float>();
        handAnim.SetFloat("Grip" , gripValue);

    }
}
