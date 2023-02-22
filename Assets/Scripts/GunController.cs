using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : XRGrabInteractable
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 30;
    public const uint MAX_AMMO = 30;
    public uint ammoCount;
    public TextMeshProUGUI ammoCountText;


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

    private void Start()
    {
        ammoCountText.enabled = false;
        ammoCount = 20;

        base.activated.AddListener(FireBullet);
        base.selectExited.AddListener(OnGunRemoved);
    }

    private void OnGunRemoved(SelectExitEventArgs args)
    {
        PlaySoundNonOverlapped(removeAudio);
        ammoCountText.enabled = false;
        transform.SetParent(null); // Reset parent transform
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (ammoCount == 0)
        {
            audioSource.PlayOneShot(outOfAmmoAudio);
            return;
        }
        audioSource.PlayOneShot(gunshotAudio);
        Quaternion bulletRotationOffset = Quaternion.Euler(90, 0, 0);
        GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation * bulletRotationOffset);
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 5);
        ammoCount -= 1;
        UpdateAmmoCountText();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        void FixTransformGun(SelectEnterEventArgs args)
        {
            if (args.interactorObject.transform.CompareTag("Left Hand"))
            {
                attachTransform = leftAttachTransform;
            }
            else if (args.interactorObject.transform.CompareTag("Right Hand"))
            {
                attachTransform = rightAttachTransform;
            }
        }
        FixTransformGun(args);

        void onGunPickedUp(SelectEnterEventArgs args)
        {
            // To prevent sound loop error (happen when another hand try to acquire the gun from a selected gun).
            PlaySoundNonOverlapped(pickupAudio);
            ammoCountText.enabled = true;
            UpdateAmmoCountText();
        }
        onGunPickedUp(args);


        transform.SetParent(args.interactorObject.transform);

        base.OnSelectEntered(args);
    }

    public void Reload(uint reloadAmount)
    {
        if (reloadAmount == 0)
        {
            audioSource.clip = outOfAmmoAudio;
            audioSource.Play();
            return;
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

        audioSource.PlayOneShot(reloadAudio);
        StartCoroutine(ReloadCoroutine(1.0f, reloadAmount));
    }

    void PlaySoundNonOverlapped(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void UpdateAmmoCountText() => ammoCountText.text = string.Format("{0}/{1}", ammoCount, MAX_AMMO);
}

