using UnityEngine;

public enum States
{
    Wandering = 0,
    Alert = 1,
    Aggressive = 2,
    Death = 3,
    Completed = 999,
}

public class Enemy_Grunt : Enemy_Statistics
{
    public States state { get; private set; } = States.Wandering;

    private Rigidbody rb;
    public GameObject enemyPickupObj;

    public float wanderSpeed;

    private float distance;
    public float timeLeftToDie;
    public float timeLeftToUpdateWander;

    private float oldDirection;
    private float directionChange;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(playerPos.position, transform.position);

        if (_damageFlashing)
            currentMaterial.material = damagedColor;

        if (currentHealth <= 0)
        {
            if (state != States.Death)
                SetState(States.Death);
            UpdateDeath();
            return;
        }

        switch (state)
        {
            case States.Wandering:
                UpdateWandering();
                break;

            case States.Alert:
                UpdateAlert();
                break;

            case States.Aggressive:
                UpdateAggressive();
                break;
        }
    }

    private void SetState(States newState)
    {
        if (state == newState)
            return;

        state = newState;

        switch (state)
        {
            case States.Wandering:
                Debug.Log("Entered Wandering");
                currentMaterial.material = idleColor;
                break;

            case States.Alert:
                Debug.Log("Entered Alert");
                currentMaterial.material = spottedColor;
                timeLeftToFire = 0f;
                fireRate = Random.Range(fireRateMin, fireRateMax);
                break;

            case States.Aggressive:
                Debug.Log("Entered Aggressive");
                currentMaterial.material = aggressiveColor;
                break;

            case States.Death:
                Debug.Log("Entered Death");
                currentMaterial.material = deathColor;
                break;
        }
    }

    private void UpdateWandering()
    {
        timeLeftToUpdateWander -= Time.deltaTime;

        Vector3 movement = transform.forward * wanderSpeed;

        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        if (timeLeftToUpdateWander <= 0f)
        {
            oldDirection = transform.eulerAngles.y;
            directionChange = RecalculateWanderDirection();

            timeLeftToUpdateWander = Random.Range(2f, 8f);
        }

        float newY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, directionChange, 100f * Time.fixedDeltaTime);

        rb.MoveRotation(Quaternion.Euler(0f, newY, 0f));

        if (distance <= aggroDistance)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            SetState(States.Alert);
        }
    }

    private void UpdateAlert()
    {
        Invoke(nameof(EnterAggressive), 0.3f);
    }

    private void EnterAggressive() //Invokes cannot take arguments, so need to use this YUCKY enter solution instead
    {
        SetState(States.Aggressive);
    }

    private void UpdateAggressive()
    {
        if (distance > aggroDistance)
        {
            SetState(States.Wandering);
            return;
        }

        timeLeftToFire += Time.deltaTime;

        if (timeLeftToFire >= fireRate)
        {
            ShootingPlayer();
            timeLeftToFire = 0f;
        }
    }

    private void UpdateDeath()
    {
        if (timeLeftToDie <= 0f || enemyPickupObj == null)
            Destroy(gameObject);

        enemyPickupObj.SetActive(true);
        timeLeftToDie -= Time.deltaTime;
    }

    //
    // TO DO: Fix up recalculating direction when caught on walls
    //
    private float RecalculateWanderDirection()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, 2f))
        {
            Vector3 awayFromWall = hit.normal;

            float awayAngle = Quaternion.LookRotation(awayFromWall).eulerAngles.y;

            return awayAngle + Random.Range(-135f, 135f);
        }

        return Random.Range(0f, 360f);
    }
}