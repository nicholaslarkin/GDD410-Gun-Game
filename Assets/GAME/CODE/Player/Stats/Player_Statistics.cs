using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(0)]
public class Player_Statistics : ActorStatistics
{
    public static Player_Statistics Instance { get; private set; }

    [SerializeField] private Player_Weapon weapon;

    public UIManager uiManager;

    public Weapon CurrentWeapon => weapon != null
        ? weapon.CurrentWeapon
        : null;

    protected override void Awake()
    {
        uiManager = FindAnyObjectByType<UIManager>();

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //uiManager = FindAnyObjectByType<UIManager>();
    }

    //
    // TO DO: Find some way to truncate this function into another script; doesn't really belong here...?
    //
    public void AddEffectFromPickup(int pickupArgument, int quantityAmount)
    {
        Player_Inventory playerInventory = GetComponent<Player_Inventory>();

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

                uiManager.UpdateWeaponUI(quantityAmount);
                break;

            // Misc
            case 3:
                Debug.Log("Misc pickup not implemented yet.");
                break;

            // Regular Hearts
            case 4:
                playerInventory.regularHearts += quantityAmount;
                uiManager.UpdateHeartCounters(playerInventory.regularHearts, playerInventory.gildedHearts);
                break;

            // Gilded Hearts
            case 5:
                playerInventory.gildedHearts += quantityAmount;
                uiManager.UpdateHeartCounters(playerInventory.regularHearts, playerInventory.gildedHearts);
                break;
        }
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        AudioManager.Instance.PlaySFXMult("Player_Hurt", 0.3f);
        AudioManager.Instance.PlaySFXMult("Hit_Splatter", 0.3f);

        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.UpdateHealthUI(currentHealth);
    }

    //SceneLoader stuff only for build...?
    public override void Death()
    {
        SceneLoader sceneLoader = FindAnyObjectByType<SceneLoader>();
        AudioManager.Instance.PlaySFX("Hit_Big_Splatter", 0.7f);
        AudioManager.Instance.PlaySFX("Player_Death", 3);
        StartCoroutine(sceneLoader.BuildLoadSceneUponDeath());
        Destroy(gameObject);

        //currentHealth = 101;
        //SceneManager.LoadScene(3);
    }
}