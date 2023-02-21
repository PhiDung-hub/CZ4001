using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    // public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 100;
    public float highOffset = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menu.transform.position = head.position + new Vector3(head.forward.x, 0 , head.forward.z).normalized * spawnDistance + new Vector3(0, highOffset, 0);
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
