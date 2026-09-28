using UnityEngine;

public class MiscPickup : Pickup, IInteractable
{
    public override void PickupItem()
    {
        Debug.Log("Picked up a random thing!");
        Destroy(gameObject);
    }
}
