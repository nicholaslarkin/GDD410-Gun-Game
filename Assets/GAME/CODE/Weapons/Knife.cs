using UnityEngine;

public class Knife : Melee
{
    private bool _canShoot = false;
    private bool _attackCooldownSet = false;
    private float currentAttackInterval;
    private float currentDeployInterval; //Hard coded based on animation; change to animation events later

    private void OnEnable()
    {
        _canShoot = false;
        _attackCooldownSet = false;
        currentAttackInterval = weaponData.attackInterval;
        currentDeployInterval = weaponData.deployInterval;

        AudioManager.Instance.PlaySFX("Knife_Equip", 0.7f);
    }

    private void Update()
    {
        currentDeployInterval -= Time.deltaTime;

        if (_attackCooldownSet)
            currentAttackInterval -= Time.deltaTime;

        if (currentAttackInterval <= 0f)
        {
            _attackCooldownSet = false;
            currentAttackInterval = weaponData.attackInterval;
        }

        if (currentDeployInterval <= 0f)
            _canShoot = true;

    }

    public override void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        base.Initialize(data, playerInventory);
    }

    public override void Shot()
    {
        if (!_canShoot)
            return;

        if (weaponData.attackInterval >= 0f && _attackCooldownSet)
            return;

        base.Shot();

        _attackCooldownSet = true;
        weaponAnim.SetBool("isAttacking", true);
        AudioManager.Instance.PlaySFX("Knife_Attack", 0.7f);

        /*if (!TryShoot(out RaycastHit hit))
            return;

        ActorStatistics target =
            hit.collider.GetComponentInParent<ActorStatistics>();

        if (target != null)
        {
            target.TakeDamage(weaponData.maxDamage);
        }*/
    }
}
