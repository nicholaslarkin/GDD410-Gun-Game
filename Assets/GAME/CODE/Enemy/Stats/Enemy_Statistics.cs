using UnityEngine;

public class Enemy_Statistics : ActorStatistics
{
    [Header("Enemy-Specifics")]
    //REWORK TO STATE MACHINE LATER
    private bool _playerSpotted;
    private bool _damageFlashing;

    [SerializeField] private Renderer currentMaterial;
    [SerializeField] private Material idleColor;
    [SerializeField] private Material spottedColor;
    [SerializeField] private Material damagedColor;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform bulletFirePoint;
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private float fireRate;
    [SerializeField] private float aggroDistance = 20f;
    private float timeLeftToFire;

    private void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        currentMaterial = GetComponentInChildren<Renderer>();
    }

    protected override void Update()
    {
        base.Update();

        float distanceCheck = Vector3.Distance(playerPos.position, transform.position);

        if (_damageFlashing)
            return;

        if (distanceCheck <= aggroDistance)
        {
            currentMaterial.material = spottedColor;
            _playerSpotted = true;
        }
        else
        {
            currentMaterial.material = idleColor;
            _playerSpotted = false;
        }
    }

    private void FixedUpdate()
    {
        if (_playerSpotted)
            timeLeftToFire += Time.deltaTime;
        else
            timeLeftToFire = fireRate / 1.5f;

        if (timeLeftToFire >= fireRate && _playerSpotted)
        {
            ShootingPlayer();
            timeLeftToFire = 0;
        }
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        currentMaterial.material = damagedColor;
        _damageFlashing = true;

        Invoke(nameof(ResetMaterial), 0.05f);
    }

    private void ResetMaterial()
    {
        _damageFlashing = false;
    }

    private void ShootingPlayer()
    {
        GameObject bullet = Instantiate(bulletObj, bulletFirePoint.position, bulletFirePoint.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(playerPos);
    }
}
