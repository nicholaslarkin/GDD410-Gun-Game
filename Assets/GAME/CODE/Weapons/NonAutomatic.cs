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

    public void ShootGun()
    {      
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, hurtbox))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 3f);
            Debug.Log("Shot a Target!");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 3f);
            Debug.Log("No Target hit!");
        }
    }
}
