using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameStateManager stateManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            stateManager.targetRemaining -= 1;
            stateManager.UpdateTargetRemainingText();
            Destroy(other.gameObject);
            base.gameObject.SetActive(false);
        }

    }
}
