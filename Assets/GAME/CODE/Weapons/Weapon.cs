using Unity.VisualScripting;
using UnityEngine;

public enum WeaponTypes
{
    Knife = 0,
    Pistol = 1,
    Shotgun = 2,
}

public enum WeaponState
{
    Idle = 0,
    Firing = 1,
    Reloading = 2,
}

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected Animator weaponAnim;

    protected Player_Inventory inventory;

    public WeaponState state { get; private set; } = WeaponState.Idle;

    public WeaponData Data => weaponData;

    public WeaponTypes WeaponType { get; private set; }

    public int AmmoClip;
    public int AmmoReserve => inventory.GetReserveAmmo(weaponData);

    private void FixedUpdate()
    {
        switch (state)
        {
            case WeaponState.Idle:
                UpdateIdle();
                break;

            case WeaponState.Firing:
                UpdateFiring();
                break;

            case WeaponState.Reloading:
                UpdateReloading();
                break;
        }
    }

    public virtual void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        weaponData = data;
        inventory = playerInventory;

        if (weaponData == null)
        {
            Debug.LogError($"{name}: Missing WeaponData!");
            return;
        }

        if (inventory == null)
        {
            Debug.LogError($"{name}: Missing Player_Inventory!");
            return;
        }

        WeaponType = weaponData.weaponType;

        AmmoClip = weaponData.ammoClip;
    }

    public virtual bool TryShoot()
    {
        switch (weaponData.weaponType)
        {
            case WeaponTypes.Knife:
                weaponAnim.SetBool("isAttacking", true);
                weaponAnim.Play("PistolFire", 0, 0f);
                return true;

            case WeaponTypes.Pistol:
                if (AmmoClip <= 0)
                    return false;

                weaponAnim.SetBool("isAttacking", true);
                weaponAnim.Play("KnifeAttack", 0, 0f);
                AmmoClip--;
                return true;
        }

        return false;
    }

    public void Reload()
    {
        if (weaponData == null || WeaponType == WeaponTypes.Knife)
            return;

        int missingAmmo = weaponData.ammoClip - AmmoClip;
        int ammoToLoad = inventory.ConsumeReserveAmmo(weaponData, missingAmmo);

        if (ammoToLoad <= 0 || weaponData.weaponType == WeaponTypes.Knife)
            return;

        weaponAnim.SetBool("isReloading", true);
        AmmoClip += ammoToLoad;
    }

    public void AddReserveAmmo(int amount)
    {
        inventory.AddReserveAmmo(weaponData, amount);
    }

    private void SetState(WeaponState newState)
    {
        if (state == newState)
            return;

        state = newState;

        switch (state)
        {
            case WeaponState.Idle:
                break;

            case WeaponState.Firing:
                break;

            case WeaponState.Reloading:
                break;
        }
    }

    protected void UpdateIdle()
    {

    }

    protected void UpdateFiring()
    {

    }

    protected void UpdateReloading()
    {

    }
}
