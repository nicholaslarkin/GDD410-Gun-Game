using UnityEngine;

public class PlayerStartingPos : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        player.transform.position = this.transform.position;
    }
}
