using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 30;
    public TextMeshProUGUI ammoCountText;
    
    public TextMeshProUGUI hitCountText;
    public uint hitCount = 0;

    const uint MAX_AMMO = 30;
    private uint ammoCount = 200;

    void Start()
    {
        XRGrabInteractable grabbedObject = GetComponent<XRGrabInteractable>();
        grabbedObject.activated.AddListener(FireBullet);
        grabbedObject.activated.AddListener(Reload);
        SetCountText();
        
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
            SetCountText();

        }
    }
    public void SetCountText()
    {   
        ammoCountText.text = "Ammo Count: " + ammoCount.ToString();
        hitCountText.text = "Target Shoot: " + hitCount.ToString();
    }



    public void Reload(ActivateEventArgs arg)
    {
        // XRController xrController = (XRController) arg.interactorObject;
        // if (xrController.IsPressed(InputHelpers.Button.PrimaryButton)) {
        // //     uint reloadAmmount = MAX_AMMO - ammoCount;
        // //     ammoCount = MAX_AMMO;
        // }

        // Some logic to 
    }


}
