using UnityEngine;

public class AmmoPickup : Pickup, IInteractable
{
    public override void PickupItem()
    {
        Debug.Log("Picked up Ammo!");
        Destroy(gameObject);
    }
}
