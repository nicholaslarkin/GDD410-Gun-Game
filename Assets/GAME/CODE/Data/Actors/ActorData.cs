using UnityEngine;

public class ActorData : ScriptableObject
{
    [Range(0, 100)]
    public int currentHealth;
    [Range(0, 100)]
    public int maxHealth;
}
