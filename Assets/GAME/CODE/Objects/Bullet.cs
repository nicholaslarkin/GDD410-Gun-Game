using System.ComponentModel;
using UnityEngine;

public class Bullet : Hitbox
{
    private Transform target;
    private Vector3 targetDirection;

    public float bulletLifetime = 3f;
    public float bulletMinSpeed = 10f;
    public float bulletMaxSpeed = 25f;
    private float bulletSpeed;
    private float bulletTimer;

    private void Awake()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        bulletSpeed = Random.Range(bulletMinSpeed, bulletMaxSpeed);
    }

    private void Update()
    {
        if (target == null)
            return;

        transform.position += targetDirection * bulletSpeed * Time.deltaTime;

        bulletTimer += Time.deltaTime;

        if (bulletTimer >= bulletLifetime)
        {
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Player_Statistics player = other.GetComponentInParent<Player_Statistics>();

        if (player != null)
        {
            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Wall")
            || other.gameObject.layer == LayerMask.NameToLayer("Grounded"))
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        targetDirection = (target.position - transform.position).normalized;
    }
}
