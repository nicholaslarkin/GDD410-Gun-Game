using UnityEngine;

public enum KnifeMods
{
    ModA,
    ModB,
}

public enum PistolMods
{
    ModC,
    ModD,
}

public enum ShotgunMods
{
    ModE,
    ModF,
}

[CreateAssetMenu(fileName = "WeaponMod", menuName = "WeaponMod")]

public class WeaponMod : ScriptableObject
{
    public WeaponTypes weaponType;
    public Texture weaponModImage;
    public int weaponModPrice;
    public string weaponModName;
    public string weaponModDescription;

    //public WeaponModAdditive[] weaponModAdditives;

    [Header("Stat Changes")]
    public float damageChange;
    public float speedChange;
    public float jumpHeightChanage;
    public float fireRateChange;
    public float ReloadChange;
}
