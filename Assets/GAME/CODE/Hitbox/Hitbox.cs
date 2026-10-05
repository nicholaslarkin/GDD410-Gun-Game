using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private int damageValue;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHurtbox"))
        {
            Debug.Log("Hitbox has found a Hurtbox!");

            ActorStatistics actor = other.GetComponentInParent<ActorStatistics>();

            if (actor != null)
            {
                actor.TakeDamage(damageValue);
            }
            else
            {
                Debug.Log("Actor is null!");
            }
        }
    }
}
