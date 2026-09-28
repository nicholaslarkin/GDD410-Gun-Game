using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] private LayerMask hurtbox;

    protected override void Awake()
    {
        base.Awake();

        hurtbox = LayerMask.GetMask("Hurtbox");
    }

    public override void ShootGun()
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
