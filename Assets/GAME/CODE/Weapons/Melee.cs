using UnityEngine;

public class Melee : Weapon
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

    public void ShootGun()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, hurtbox))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Shot a Target!");
        }
        else
        {
            Debug.Log("No Target hit!");
        }
    }
}
