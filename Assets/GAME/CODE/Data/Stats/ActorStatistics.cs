using System;
using UnityEngine;

public class ActorStatistics : MonoBehaviour
{
    [SerializeField] private ActorData actorData;

    //[SerializeField] private int maxHealth;

    public ActorData DATA => actorData;

    public int currentHealth { get; protected set; }
    public int maxHealth { get; protected set; }

    //public event Action<int> OnHealthChanged;

    protected virtual void Awake()
    {
        currentHealth = actorData.currentHealth;
        maxHealth = actorData.maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
            Death();

        if (currentHealth > maxHealth)
        {
            Debug.Log("Health set to Max");
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int amount) //Positive number for damage, negative number for healing
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

    void Death()
    {
        Destroy(gameObject);
    }
}
