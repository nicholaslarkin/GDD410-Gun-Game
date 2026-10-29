using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] private LayerMask hurtbox;

    protected virtual void Awake()
    {
        hurtbox = LayerMask.GetMask("Hurtbox");
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
