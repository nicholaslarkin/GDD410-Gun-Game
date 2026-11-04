using UnityEngine;

public class Knife : Melee
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        Debug.Log(state);
    }

    public override void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        base.Initialize(data, playerInventory);
    }

    public override void Shot()
    {
        weaponAnim.SetBool("isAttacking", true);
        weaponAnim.Play("KnifeAttack", 0, 0f);

        if (!TryShoot(out RaycastHit hit))
            return;

        ActorStatistics target =
            hit.collider.GetComponentInParent<ActorStatistics>();

        if (target != null)
        {
            target.TakeDamage(weaponData.maxDamage);
        }
    }

    /*public override void Reload()
    {
        base.Reload();
    }

    protected override void UpdateIdle()
    {
        if (TryShoot())
        {
            state = WeaponState.Firing;
        }
    }

    protected override void UpdateFiring()
    {
        // Wait for the attack animation to finish.
        // Then return to Idle.
    }*/
}
