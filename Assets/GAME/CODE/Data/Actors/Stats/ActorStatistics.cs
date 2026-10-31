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

    public int currentHealth;
    public int maxHealth;

    //public event Action<int> OnHealthChanged;

    protected virtual void Awake()
    {
        currentHealth = actorData.currentHealth;
        maxHealth = actorData.maxHealth;
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

        //health = Mathf.Clamp(health - amount, 0, maxHealth);

        currentHealth = currentHealth - amount;

        //OnHealthChanged?.Invoke(health);
        if (actorData.name == "PlayerData")
        {
            UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            uiManager.UpdateHealthUI(currentHealth);
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
