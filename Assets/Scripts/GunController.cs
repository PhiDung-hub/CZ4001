using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 30;

    const uint MAX_AMMO = 30;
    private uint ammoCount = 30;

    void Start()
    {
        XRGrabInteractable grabbedObject = GetComponent<XRGrabInteractable>();
        grabbedObject.activated.AddListener(FireBullet);
        grabbedObject.activated.AddListener(Reload);
    }

    // Update is called once per frame
    void Update() { }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (ammoCount > 0)
        {
            Quaternion bulletRotationOffset = Quaternion.Euler(90, 0, 0);
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation * bulletRotationOffset);
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 5);
            ammoCount -= 1;
        }
    }

    public void Reload(ActivateEventArgs arg)
    {
        // if (arg.interactorObject is XRController)
        // {
        //     Debug.Log("A Button pressed, reloading");
        // }

        // if (arg.interactorObject is XRController xRController &&
        //     xrController.isPressed(InputHelpers.Button.PrimaryButton))
        // {

        //     uint reloadAmmount = MAX_AMMO - ammoCount;
        //     ammoCount = MAX_AMMO;
        // }


        // Some logic to 
    }


}
