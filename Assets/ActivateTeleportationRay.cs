using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActivate;
    public InputActionProperty leftCancel;
    
    public InputActionProperty rightActivate;
    public InputActionProperty rightCancel;

    const float threshold = 0.15f;
    void Update()
    {
        leftTeleportation.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > threshold);
        rightTeleportation.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > threshold);
    }
}
