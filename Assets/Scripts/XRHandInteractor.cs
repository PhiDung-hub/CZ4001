using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHandInteractor : XRDirectInteractor
{
    public InputActionProperty reloadGunAction;

    GunController Gun;
    AmmoController Ammo;
    public PlayerStateManager Player;


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Gun"))
        {
            // Picked up and assign a reference to the Gun object
            Gun = (GunController)args.interactableObject;
        }
        else if (args.interactableObject.transform.CompareTag("Ammo"))
        {
            // TODO: Prevent Ammo from being picked up multiple times
            Ammo = (AmmoController)args.interactableObject;
            Player.ammoCount += Ammo.bulletContained;
            Player.UpdateAmmoCountText();
        }
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Gun"))
        {
            Gun = null;
        }
        base.OnSelectExited(args);
    }

    void Update()
    {
        if (Gun is not null && reloadGunAction.action.WasReleasedThisFrame())
        {
            uint reloadAmount = Math.Min(Player.ammoCount, GunController.MAX_AMMO - Gun.ammoCount);
            Player.ammoCount -= reloadAmount;
            Gun.Reload(reloadAmount);
            Player.UpdateAmmoCountText();
        }
    }
}
