using UnityEngine;

/// <summary>
/// Actual weapon instances (holds basic weapon functions that has inherited scripts for unique functions
/// </summary>

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

    private LayerMask hurtbox;
    private GameObject raycastPointer;

    protected Player_Inventory inventory;

    protected UIManager uiManager;

    public WeaponState state { get; protected set; } = WeaponState.Idle;

    public WeaponData Data => weaponData;

    public WeaponTypes WeaponType { get; private set; }

    public int AmmoClip;
    public int AmmoReserve => inventory.GetReserveAmmo(weaponData);

    protected virtual void Awake()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        hurtbox = LayerMask.GetMask("Hurtbox");
        raycastPointer = GameObject.Find("RaycastPointer");
    }

    /*private void Update()
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
    }*/

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

    public virtual bool TryShoot(out RaycastHit hit)
    {
        Vector3 origin = raycastPointer.transform.position;
        Vector3 direction = raycastPointer.transform.forward;

        if (Physics.SphereCast(origin, weaponData.hitRadius, direction, out hit, weaponData.maxRange, hurtbox))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.green, 3f);
            return true;
        }

        Debug.DrawRay(origin, direction * weaponData.maxRange, Color.red, 3f);
        return false;

        /*switch (weaponData.weaponType)
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

        return false;*/
    }

    public virtual void Shot()
    {
        if (!TryShoot(out RaycastHit hit))
            return;

        ActorStatistics target =
            hit.collider.GetComponentInParent<ActorStatistics>();

        if (target == null)
            return;

        WeaponData data = weaponData;

        float distancePercent = Mathf.Clamp01(
            hit.distance / Mathf.Max(data.maxRange, 0.001f)
        );

        float falloff = Mathf.Pow(distancePercent, 2f);

        int damageCalc = Mathf.RoundToInt(
            Mathf.Lerp(data.maxDamage, data.minDamage, falloff)
        );

        if (damageCalc > 0)
        {
            target.TakeDamage(damageCalc);

            Debug.Log(
                $"{damageCalc} damage dealt at {hit.distance:F1} units."
            );
        }
    }

    public virtual void Reload()
    {
        if (weaponData == null || WeaponType == WeaponTypes.Knife)
            return;

        int missingAmmo = weaponData.ammoClip - AmmoClip;
        int ammoToLoad = inventory.ConsumeReserveAmmo(weaponData, missingAmmo);

        if (ammoToLoad <= 0 || weaponData.weaponType == WeaponTypes.Knife)
            return;

        weaponAnim.SetBool("isReloading", true);
        AmmoClip += ammoToLoad;
        uiManager.UpdateWeaponUI(AmmoClip);
    }

    public void AddReserveAmmo(int amount)
    {
        inventory.AddReserveAmmo(weaponData, amount);
    }

    protected virtual void UpdateIdle()
    {

    }

    protected virtual void UpdateFiring()
    {

    }

    protected virtual void UpdateReloading()
    {

    }
}
