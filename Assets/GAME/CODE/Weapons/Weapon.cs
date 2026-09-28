using UnityEditor;
using UnityEngine;

public enum WeaponItem
{
    Knife = 1,
    Pistol = 2,
    Shotgun = 3,
}
public enum WeaponTypes
{
    Melee = 0,
    Non_Automatic = 1,
    Burst = 2,
}

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    public WeaponTypes weaponType;
    public GameObject weaponModel;
    public int ammoClip;
    public int ammoReserve;

    protected virtual void Awake()
    {
        if (weaponData == null)
            return;

        weaponType = weaponData.weaponType;
        ammoClip = weaponData.ammoClip;
        ammoReserve = weaponData.ammoReserve;
    }

    public virtual void ShootGun()
    {
        ammoClip--;
    }
}
