using JetBrains.Annotations;
using UnityEngine;

public class Pistol : NonAutomatic
{
    private int ammoToLoad;
    private bool _canShoot = false;
    private bool _attackCooldownSet = false;
    private bool _reloadCooldownSet = false;
    private float currentAttackInterval;
    private float currentReloadInterval; //Hard coded based on animation; change to animation events later
    private float currentDeployInterval; //Hard coded based on animation; change to animation events later

    private void OnEnable()
    {
        _canShoot = false;
        _attackCooldownSet = false;
        _reloadCooldownSet = false;
        currentAttackInterval = weaponData.attackInterval;
        currentReloadInterval = weaponData.reloadInterval;
        currentDeployInterval = weaponData.deployInterval;

        AudioManager.Instance.PlaySFX("Pistol_Equip", 0.7f);
    }

    private void Update()
    {
        currentDeployInterval -= Time.deltaTime;

        if (_attackCooldownSet)
            currentAttackInterval -= Time.deltaTime;

        if (_reloadCooldownSet)
            currentReloadInterval -= Time.deltaTime;

        if (currentAttackInterval <= 0f)
        {
            _attackCooldownSet = false;
            currentAttackInterval = weaponData.attackInterval;

            if (AmmoClip <= 0)
                Reload();
        }

        if (currentReloadInterval <= 0f)
        {
            int missingAmmo = weaponData.ammoClip - AmmoClip;
            ammoToLoad = inventory.ConsumeReserveAmmo(weaponData, missingAmmo);

            AmmoClip += ammoToLoad;
            uiManager.UpdateWeaponUI(AmmoClip);

            _reloadCooldownSet = false;
            currentReloadInterval = weaponData.reloadInterval;
        }

        if (currentDeployInterval <= 0f)
        {
            _canShoot = true;

            if (AmmoClip <= 0)
                Reload();
        }

    }

    public override void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        base.Initialize(data, playerInventory);
    }

    public override void Shot()
    {
        if (!_canShoot)
            return;

        if (_reloadCooldownSet)
            return;

        if (weaponData.attackInterval >= 0f && _attackCooldownSet)
            return;

        base.Shot();

        _attackCooldownSet = true;
        AudioManager.Instance.PlaySFX("Gun_Fire", 0.7f);
        weaponAnim.SetBool("isAttacking", true);
        weaponAnim.SetBool("isReloading", false);
        weaponAnim.Play("PistolFire", 0, 0f);
        AmmoClip--;
        uiManager.UpdateWeaponUI(AmmoClip);
    }

    public override void Reload()
    {
        if (weaponData == null || !_canShoot)
            return;

        if (_attackCooldownSet || _reloadCooldownSet)
            return;

        if (AmmoClip == weaponData.ammoClip)
        {
            return;
        }

        AudioManager.Instance.PlaySFX("Shotgun_Pump", 0.7f);
        weaponAnim.SetBool("isReloading", true);
        _reloadCooldownSet = true;
    }
}
