using UnityEngine;
using UnityEngine.UI;

public class WeaponData : ScriptableObject
{
    public WeaponTypes weaponType;

    public Texture weaponImage;

    public GameObject weaponModel;

    public int ammoClip;
    public int ammoReserve;

    public int damage;
}
