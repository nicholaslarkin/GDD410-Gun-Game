using UnityEngine;
using UnityEngine.UI;

public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public WeaponTypes weaponType;

    public Texture weaponImage;

    public Texture ammoImage;

    public GameObject weaponModel;

    [Header("Reload")]
    public float reloadTime;

    [Header("Ammo")]
    public int ammoClip;
    public int ammoReserve;

    [Header("Damage")]
    public int minDamage;
    public int maxDamage;

    [Header("Range")]
    public float maxRange = 24f;

    [Header("Radius")]
    public float hitRadius;

    [Header("Deploy Interval")]
    public float deployInterval;

    [Header("Attack Interval")]
    public float attackInterval;

    [Header("Reload Interval")]
    public float reloadInterval;
}
