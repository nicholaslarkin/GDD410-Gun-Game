using UnityEngine;
using System.Collections;

public class Enemy_Statistics : ActorStatistics
{
    [Header("Enemy-Specifics")]
    protected bool _damageFlashing;

    [SerializeField] protected Renderer currentMaterial;
    [SerializeField] protected Material idleColor;
    [SerializeField] protected Material spottedColor;
    [SerializeField] protected Material aggressiveColor;
    [SerializeField] protected Material damagedColor;
    [SerializeField] protected Material deathColor;
    [SerializeField] protected Transform playerPos;
    [SerializeField] protected Transform bulletFirePoint;
    [SerializeField] protected GameObject bulletObj;
    [SerializeField] protected float fireRateMin;
    [SerializeField] protected float fireRateMax;
    [SerializeField] protected float aggroDistance = 20f;
    protected float fireRate;
    public float timeLeftToFire;

    private void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        currentMaterial = GetComponentInChildren<Renderer>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
            return;

        base.TakeDamage(amount);

        Material lastColor = currentMaterial.material;
        currentMaterial.material = damagedColor;
        _damageFlashing = true;

        StartCoroutine(ResetMaterial(lastColor));
    }

    protected virtual IEnumerator ResetMaterial(Material lastColor)
    {
        yield return new WaitForSeconds(0.1f);

        _damageFlashing = false;
        currentMaterial.material = lastColor;
    }

    protected void ShootingPlayer()
    {
        GameObject bullet = Instantiate(bulletObj, bulletFirePoint.position, bulletFirePoint.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(playerPos);
    }

    public override void Death()
    {
        currentMaterial.material = deathColor;
        return;
    }
}
