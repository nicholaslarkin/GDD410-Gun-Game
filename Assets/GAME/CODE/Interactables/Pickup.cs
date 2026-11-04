using UnityEngine;

/// <summary>
/// Allows picking up items by touching them (derived pickup functions for each type of pickup)
/// </summary>

public class Pickup : MonoBehaviour
{
    public int quantityAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHurtbox"))
        {
            PickupItem();
        }
    }

    public virtual void PickupItem()
    {
        
    }
}
