using UnityEngine;

public enum WeaponTypes
{
    Knife = 0,
    Pistol = 1,
    Shotgun = 2,
}

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;

    public WeaponData Data => weaponData;

    public WeaponTypes WeaponType { get; private set; }

    public int AmmoClip { get; private set; }
    public int AmmoReserve { get; private set; }

    public virtual void Initialize(WeaponData data)
    {
        weaponData = data;

        if (weaponData == null)
        {
            Debug.LogError($"{name}: Missing WeaponData!");
            return;
        }

        WeaponType = weaponData.weaponType;
        AmmoClip = weaponData.ammoClip;
        AmmoReserve = weaponData.ammoReserve;
    }

    public bool TryShoot()
    {
        if (AmmoClip <= 0)
            return false;

        AmmoClip--;
        return true;
    }

    public void Reload()
    {
        int missingAmmo = weaponData.ammoClip - AmmoClip;
        int ammoToLoad = Mathf.Min(missingAmmo, AmmoReserve);

        if (ammoToLoad <= 0)
            return;

        AmmoClip += ammoToLoad;
        AmmoReserve -= ammoToLoad;
    }

    public void AddReserveAmmo(int amount)
    {
        if (amount > 0)
            AmmoReserve += amount;
    }
}
