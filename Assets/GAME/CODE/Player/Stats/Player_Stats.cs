using JetBrains.Annotations;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class Player_Stats : MonoBehaviour /*IDamageable*/
{
    public Player_Data playerData;
    public WeaponData currentWeapon;
    public Player_Weapon weapon;

    public int hp;
    public int ammoCount;
    public int ammoReserve;

    private void Awake()
    {
        hp = playerData.hp;
        ammoCount = currentWeapon.ammoClip;
        ammoReserve = currentWeapon.ammoReserve;
    }

    public virtual void ShootGun()
    {
        ammoCount--;
    }

    public void ChangeWeaponPrefab(WeaponData weapon)
    {
        currentWeapon = weapon;

        ammoCount = currentWeapon.ammoClip;
        ammoReserve = currentWeapon.ammoReserve;
    }

    void Damage(int amount)
    {
        hp -= amount;
    }
}
