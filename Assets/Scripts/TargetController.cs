using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TargetController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stateManager;
    private GameStateManager state_info;
    public AudioSource audioSource;
    public AudioClip hitAudio;
    // public GameObject attackText;
    // public float offset_x;
    // public float offset_y;
    // public float offset_z;
    void Start()
    {
        state_info = stateManager.GetComponent<GameStateManager>();
        audioSource = GetComponent<AudioSource>();
    }

    bool isHit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            // GameObject showingTextObject = Instantiate(attackText) as GameObject;
            // Vector3 offset = new Vector3( offset_x, offset_y, offset_z );
            // showingTextObject.transform.localPosition = gameObject.transform.position + offset;
            // showingTextObject.transform.localRotation = gameObject.transform.localRotation;            
            // Destroy(showingTextObject, 2);
            if (!isHit)
            {
                isHit = true;
                state_info.targetRemaining -= 1;
                state_info.UpdateTargetRemainingText();
                audioSource.clip = hitAudio;
                audioSource.volume = 0.5f;
                audioSource.Play();
                Destroy(gameObject, hitAudio.length);
                Destroy(other.gameObject);
                // base.gameObject.SetActive(false);
            }
        }


    }
}
