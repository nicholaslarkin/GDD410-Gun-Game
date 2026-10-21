using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(0)]
public class Player_Statistics : ActorStatistics
{
    public static Player_Statistics Instance { get; private set; }

    [SerializeField] private Player_Weapon weapon;

    public int regularHearts;
    public int gildedHearts;

    private UIManager uiManager;

    public Weapon CurrentWeapon => weapon != null
        ? weapon.CurrentWeapon
        : null;

    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        base.Awake();

        if (weapon == null)
            weapon = GetComponent<Player_Weapon>();
    }

    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();

        UpdateWeaponUI();
    }

    public virtual void Shot(bool hurtboxDetected, RaycastHit hit)
    {
        Weapon current = CurrentWeapon;

        if (current == null || !current.TryShoot())
        {
            UpdateWeaponUI();
            return;
        }

        if (hurtboxDetected)
        {
            WeaponData data = current.Data;

            float maxDistance = Mathf.Max(data.maxRange, 0.001f);
            float distancePercent = Mathf.Clamp01(
                hit.distance / maxDistance
            );

            float falloff = Mathf.Pow(distancePercent, 2f);

            int damageCalc = Mathf.RoundToInt(
                Mathf.Lerp(
                    data.maxDamage,
                    data.minDamage,
                    falloff
                )
            );

            if (damageCalc > 0)
            {
                ActorStatistics target =
                    hit.collider.GetComponentInParent<ActorStatistics>();

                if (target != null)
                {
                    target.TakeDamage(damageCalc);

                    Debug.Log(
                        $"{damageCalc} damage dealt at {hit.distance:F1} units."
                    );
                }
            }
        }

        UpdateWeaponUI();
    }

    public void Reload()
    {
        Weapon current = CurrentWeapon;

        if (current == null)
            return;

        current.Reload();

        UpdateWeaponUI();
    }

    public void UpdateWeaponUI()
    {
        if (uiManager == null)
            uiManager = FindAnyObjectByType<UIManager>();

        if (uiManager == null)
            return;

        Weapon current = CurrentWeapon;

        if (current != null)
            uiManager.UpdateWeaponUI(current.AmmoClip);
    }

    public void Hearts()
    {
        if (uiManager == null)
            uiManager = FindAnyObjectByType<UIManager>();

        if (uiManager != null)
            uiManager.UpdateHeartCounters(regularHearts, gildedHearts);
    }

    public void AddEffectFromPickup(int pickupArgument, int quantityAmount)
    {
        if (uiManager == null)
            uiManager = FindAnyObjectByType<UIManager>();

        switch (pickupArgument)
        {
            // Health
            case 1:
                currentHealth += quantityAmount;

                if (uiManager != null)
                    uiManager.UpdateHealthUI(currentHealth);
                break;

            // Ammo for the currently equipped weapon
            case 2:
                Weapon current = CurrentWeapon;

                if (current != null)
                    current.AddReserveAmmo(quantityAmount);

                UpdateWeaponUI();
                break;

            // Misc
            case 3:
                Debug.Log("Misc pickup not implemented yet.");
                break;

            // Regular Hearts
            case 4:
                regularHearts += quantityAmount;
                Hearts();
                break;

            // Gilded Hearts
            case 5:
                gildedHearts += quantityAmount;
                Hearts();
                break;
        }
    }

    public override void Death()
    {
        //Destroy(gameObject);

        currentHealth = 101;
        SceneManager.LoadScene(0);
    }
}