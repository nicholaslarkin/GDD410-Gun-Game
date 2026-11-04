using UnityEngine;

/// <summary>
/// Scene specific player teleport that spawns the player at the object's location
/// Required since player(DoNotDestroyOnLoad) maintains the same coordinate through scene transitions
/// </summary>

public class PlayerStartingPos : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        player.transform.position = this.transform.position;
    }
}
