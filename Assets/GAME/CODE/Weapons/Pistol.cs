using JetBrains.Annotations;
using UnityEngine;

public class Pistol : NonAutomatic
{
    //[SerializeField] private WeaponData pistolData;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        base.Initialize(data, playerInventory);
    }
}
