using UnityEngine;

public class NonAutomatic : Weapon
{
    [SerializeField] private LayerMask hurtbox;

    protected virtual void Awake()
    {
        hurtbox = LayerMask.GetMask("Hurtbox");
    }

    public override void Initialize(WeaponData data)
    {
        base.Initialize(weaponData);
    }
}
