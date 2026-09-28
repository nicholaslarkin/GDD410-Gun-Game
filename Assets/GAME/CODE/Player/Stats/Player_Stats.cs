using JetBrains.Annotations;
using UnityEngine;

public class Player_Stats : MonoBehaviour /*IDamageable*/
{
    public Player_Data playerData;
    public WeaponData currentWeapon;

    public int hp;
    public int ammoCount;
    public int ammoReserve;

    private void Awake()
    {
        hp = playerData.hp;
        ammoCount = currentWeapon.ammoClip;
        ammoReserve = currentWeapon.ammoReserve;
    }

    void Damage(int amount)
    {
        hp -= amount;
    }
}
