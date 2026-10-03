using UnityEngine;

public class AmmoPickup : Pickup, IInteractable
{
    public override void PickupItem()
    {
        Debug.Log("Picked up Ammo!");
        Player_Statistics playerActor = GameObject.Find("Player").GetComponent<Player_Statistics>();
        playerActor.AddEffectFromPickup(2, quantityAmount);
        Destroy(gameObject);
    }
}
