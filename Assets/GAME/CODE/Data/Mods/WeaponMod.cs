using UnityEngine;

[CreateAssetMenu(fileName = "WeaponMod", menuName = "WeaponMod")]
public class WeaponMod : ScriptableObject
{
    public WeaponTypes weaponType;
    public Texture weaponImage;

    //public WeaponModAdditive[] weaponModAdditives;

    [Header("Stat Changes")]
    public float damageChange;
    public float speedChange;
    public float jumpHeightChanage;
    public float fireRateChange;
    public float ReloadChange;
}
