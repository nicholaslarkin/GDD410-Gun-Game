using UnityEngine;

public class Knife : Melee
{
    //[SerializeField] private WeaponData knifeData;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize(WeaponData data, Player_Inventory playerInventory)
    {
        base.Initialize(data, playerInventory);
    }

    public override bool TryShoot()
    {
        return base.TryShoot();
    }
}
