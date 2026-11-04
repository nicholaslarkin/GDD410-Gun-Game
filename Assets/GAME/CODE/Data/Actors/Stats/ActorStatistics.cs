using System;
using UnityEngine;

/// <summary>
/// Handles basic actor functions (taking damage and dying; things all actors possess as a player, 
/// enemy, maybe breakable doors?)
/// </summary>

public class ActorStatistics : MonoBehaviour
{
    [SerializeField] private ActorData actorData;

    //[SerializeField] private int maxHealth;

    public ActorData DATA => actorData;

    [HideInInspector] public int currentHealth;
    public int maxHealth;

    //public event Action<int> OnHealthChanged;

    protected virtual void Awake()
    {
        //NOTE: Revert to SO later, using inspector setting health for now

        //currentHealth = actorData.currentHealth;
        //maxHealth = actorData.maxHealth;

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        if (currentHealth <= 0)
            Death();

        if (currentHealth > maxHealth)
        {
            Debug.Log("Health set to Max");
            currentHealth = maxHealth;
        }
    }

    public virtual void TakeDamage(int amount) //Positive number for damage, negative number for healing
    {
        Debug.Log("TakeDamage Ran!");

        currentHealth = currentHealth - amount;
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
