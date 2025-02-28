using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBullets : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;

    void Start()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.activated.AddListener(FireBulletFunction);
    }

    private void FireBulletFunction(ActivateEventArgs arg)
    {
        GameObject tempBullet = Instantiate(bullet , firePoint.position , transform.rotation);
        tempBullet.AddComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
        Destroy(tempBullet , 3);
    }
}
