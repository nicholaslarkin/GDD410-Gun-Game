using UnityEngine;

/// <summary>
/// Holds data for actors (players and enemies derive from this to write their own specific data)
/// </summary>

public class ActorData : ScriptableObject
{
    [Range(0, 100)]
    public int currentHealth;
    [Range(0, 100)]
    public int maxHealth;
}
