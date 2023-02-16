using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoPoints : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Left Hand grabbed the gun
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        // Right Hand grabbed the gun
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }


        // continue XRGrabInteratable logics;
        base.OnSelectEntered(args);
    }
}
