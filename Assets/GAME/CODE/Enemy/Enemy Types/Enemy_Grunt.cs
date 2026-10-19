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

    public GameObject enemyPickupObj;

    private float distance;
    public float timeLeftToDie;

    protected override void Update()
    {
        base.Update();

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
        if (distance <= aggroDistance)
            SetState(States.Alert);
    }

    private void UpdateAlert()
    {
        Invoke(nameof(EnterAggressive), 0.3f);
    }

    private void EnterAggressive() //Invokes cannot take arguments, so need to use this YUCKY solution instead
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
        enemyPickupObj.SetActive(true);
        timeLeftToDie -= Time.deltaTime;

        if (timeLeftToDie <= 0f)
            Destroy(gameObject);
    }
}