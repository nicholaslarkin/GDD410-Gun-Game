using UnityEngine;

public class MiscPickup : Pickup, IInteractable
{
    public override void PickupItem()
    {
        Debug.Log("Picked up a random thing!");
        Player_Statistics playerActor = GameObject.Find("Player").GetComponent<Player_Statistics>();
        playerActor.AddEffectFromPickup(3, quantityAmount);
        Destroy(gameObject);
    }
}
