using JetBrains.Annotations;
using UnityEngine;

public class Pistol : NonAutomatic
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        base.Initialize(data, playerInventory);
    }

    public override void Shot()
    {
        if (AmmoClip <= 0)
            return;

        base.Shot();

        weaponAnim.SetBool("isAttacking", true);
        weaponAnim.Play("PistolFire", 0, 0f);
        AmmoClip--;
        uiManager.UpdateWeaponUI(AmmoClip);
    }
}
