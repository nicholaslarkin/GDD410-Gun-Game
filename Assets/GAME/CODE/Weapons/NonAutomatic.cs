using UnityEngine;

public class NonAutomatic : Weapon
{
    public override void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        base.Initialize(data, playerInventory);
    }
}
