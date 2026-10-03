using UnityEngine;

public class HealthPickup : Pickup, IInteractable
{
    public override void PickupItem()
    {
        Debug.Log("Picked up Health!");
        Player_Statistics playerActor = GameObject.Find("Player").GetComponent<Player_Statistics>();
        playerActor.AddEffectFromPickup(1, quantityAmount);
        Destroy(gameObject);
    }
}
