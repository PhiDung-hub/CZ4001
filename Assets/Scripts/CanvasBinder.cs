using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasBinder : MonoBehaviour
{
    public Transform playerHead;
    public GameObject canvas;
    const float SPAWN_DISTANCE = 1.0f;

    void Update()
    {
        base.transform.position = playerHead.position + new Vector3(playerHead.forward.x, 0, playerHead.forward.z).normalized * SPAWN_DISTANCE;
        base.transform.LookAt(new Vector3(playerHead.position.x, base.transform.position.y, playerHead.position.z));
        base.transform.forward *= -1;
    }
}
