using UnityEngine;

public class EnemyPickup : Pickup, IInteractable
{
    private Enemy_Grunt enemyGrunt; //Refactor later to work with other enemies
    private float maxTimeLeftToDie;

    private void Awake()
    {
        enemyGrunt = GetComponentInParent<Enemy_Grunt>();
    }

    private void Start()
    {
        maxTimeLeftToDie = enemyGrunt.timeLeftToDie;
    }

    public override void PickupItem()
    {
        Debug.Log("Picked up Heart from Enemy!");
        Player_Statistics playerActor = GameObject.Find("Player").GetComponent<Player_Statistics>();
        if (enemyGrunt.timeLeftToDie <= maxTimeLeftToDie / 2)
        {
            playerActor.AddEffectFromPickup(4, quantityAmount);
            Destroy(gameObject);
        }
        else if (enemyGrunt.timeLeftToDie >= maxTimeLeftToDie / 2)
        {
            playerActor.AddEffectFromPickup(5, quantityAmount);
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
}
