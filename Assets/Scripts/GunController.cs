using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : XRGrabInteractable
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 30;
    const uint MAX_AMMO = 30;
    public uint ammoCount = 0;
    public InputActionProperty reloadAction;
    public TextMeshProUGUI ammoCountText;

    private string interactorTag = "";

    // Audio utils
    public AudioSource audioSource;
    public AudioClip gunshotAudio;
    public AudioClip outOfAmmoAudio;
    public AudioClip reloadAudio;
    public AudioClip pickupAudio;
    public AudioClip removeAudio;

    // transform gun
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Fix transform for attach object
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }

        // continue XRGrabInteratable logics;
        base.OnSelectEntered(args);
    }

    void Start()
    {
        // Fire Bullet
        base.activated.AddListener(FireBullet);

        // Ammo management
        UpdateAmmoCountText();
        ammoCountText.enabled = false;
        base.selectEntered.AddListener(onGunPickedUp);
        base.selectExited.AddListener(onGunRemoved);
    }

    // Update is called once per frame
    void Update()
    {
        bool reloadActionTrigged = reloadAction.action.WasPressedThisFrame();
        bool isGunSelected = base.isSelected;
        if (reloadActionTrigged && isGunSelected)
        {
        }
    }


    void UpdateAmmoCountText()
    {
        ammoCountText.text = string.Format("Ammo: {0}/{1}", ammoCount, MAX_AMMO);
    }

    void onGunPickedUp(SelectEnterEventArgs args)
    {
        audioSource.PlayOneShot(pickupAudio);
        ammoCountText.enabled = true;
        ammoCount = PlayerController.ammoCount;
        interactorTag = args.interactorObject.transform.tag;
        Debug.Log(interactorTag);
    }

    void onGunRemoved(SelectExitEventArgs args)
    {
        audioSource.PlayOneShot(removeAudio);
        ammoCountText.enabled = false;
        interactorTag = "";
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (ammoCount > 0)
        {
            audioSource.PlayOneShot(gunshotAudio);
            Quaternion bulletRotationOffset = Quaternion.Euler(90, 0, 0);
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation * bulletRotationOffset);
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 5);
            ammoCount -= 1;
            UpdateAmmoCountText();
        }
        else
        {
            audioSource.PlayOneShot(outOfAmmoAudio);

        }
    }


    public void Reload(uint reloadAmount)
    {
        audioSource.PlayOneShot(reloadAudio);
        StartCoroutine(ReloadCoroutine(1.0f, reloadAmount));
    }

    IEnumerator ReloadCoroutine(float reloadTime, uint reloadAmount)
    {
        yield return new WaitForSeconds(reloadTime);

        if (reloadAmount > MAX_AMMO - ammoCount)
        {
            ammoCount = MAX_AMMO;
        }
        else
        {
            ammoCount += reloadAmount;
        }
        UpdateAmmoCountText();
    }
}

