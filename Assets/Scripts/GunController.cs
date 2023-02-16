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

    private uint ammoCount = 30;

    void Start()
    {
        XRGrabInteractable grabbedObject = GetComponent<XRGrabInteractable>();
        grabbedObject.activated.AddListener(FireBullet);
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


}
