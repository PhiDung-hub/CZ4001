using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stateManager;
    private GameStateManager state_info;
    public GameObject attackText;
    public float offset_x;
    public float offset_y;
    public float offset_z;
    void Start()
    {
        state_info = stateManager.GetComponent<GameStateManager>();
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GameObject showingTextObject = Instantiate(attackText) as GameObject;
            Vector3 offset = new Vector3( offset_x, offset_y, offset_z );
            showingTextObject.transform.localPosition = gameObject.transform.position + offset;
            // showingTextObject.transform.localScale = gameObject.transform.localScale;            
            showingTextObject.transform.localRotation = gameObject.transform.localRotation;            
            Debug.Log(showingTextObject.transform.position);
            Destroy(showingTextObject, 2);
            gameObject.SetActive(false);
            state_info.targetRemaining -= 1;
            state_info.UpdateTargetRemainingText();
            // stateManager.targetRemaining -= 1;
            // stateManager.UpdateTargetRemainingText();
            // Destroy(other.gameObject);
            // base.gameObject.SetActive(false);
        }

    }
}
