using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AmmoController : XRGrabInteractable
{
    public uint bulletContained = 30;

    public AudioSource audioSource;
    public AudioClip pickupAudio;

    public GameObject ammoObject;
    void Start()
    {
        base.selectEntered.AddListener(onPickedUp);
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void onPickedUp(SelectEnterEventArgs args)
    {
        bulletContained = 0;
        audioSource.clip = pickupAudio;
        audioSource.Play();
        Destroy(ammoObject, pickupAudio.length);
    }
}
