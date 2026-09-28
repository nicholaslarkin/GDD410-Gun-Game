using UnityEngine;

public class HealthPickup : Pickup, IInteractable
{
    public override void PickupItem()
    {
        Debug.Log("Picked up Health!");
        Destroy(gameObject);
    }
}
