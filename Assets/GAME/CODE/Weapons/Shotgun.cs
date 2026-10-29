using JetBrains.Annotations;
using UnityEngine;

public class Shotgun : NonAutomatic
{
    //[SerializeField] private WeaponData shotgunData;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        base.Initialize(data, playerInventory);
    }
}
