using System;
using TMPro;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public TextMeshProUGUI ammoCountText;
    public uint ammoCount = 30;

    public AudioSource backgroundAudioSource;
    public AudioClip backgroundAudio;
    void Start()
    {
        UpdateAmmoCountText();
        backgroundAudioSource.PlayOneShot(backgroundAudio);
        backgroundAudioSource.PlayScheduled(AudioSettings.dspTime + backgroundAudio.length);
    }

    public void UpdateAmmoCountText()
    {
        ammoCountText.text = string.Format("{0}", ammoCount);
    }
}
