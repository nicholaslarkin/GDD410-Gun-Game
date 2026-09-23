using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float shootDistance;
    [SerializeField] private LayerMask hurtbox;

    private void Awake()
    {
        hurtbox = LayerMask.GetMask("Hurtbox");
    }

    public void ShootGun()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, shootDistance, hurtbox))

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
