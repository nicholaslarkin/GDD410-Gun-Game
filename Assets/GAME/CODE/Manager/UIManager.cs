using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the main gameplay UI (reads other scripts and runs functions in them to update the UI)
/// </summary>

[DefaultExecutionOrder(1)]
public class UIManager : MonoBehaviour
{
    public Player_Statistics playerStats;
    public Player_Inventory playerInventory;
    public Player_Weapon currentWeapon;
    public Weapon weaponSlot;

    public RawImage weaponImage;
    //public RawImage ammoClipImageUI;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI ammoReserveText;

    public TextMeshProUGUI regularHeartsText;
    public TextMeshProUGUI gildedHeartsText;

    public GameObject ammoClipImage;
    public Transform ammoClipPos;

    public int ammoReserve;

    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<Player_Statistics>();
        playerInventory = GameObject.Find("Player").GetComponent<Player_Inventory>();
        currentWeapon = GameObject.Find("Player").GetComponent<Player_Weapon>();
    }

    private void Start()
    {
        UpdateWeaponUI(currentWeapon.CurrentWeapon.AmmoClip);
        UpdateHealthUI(playerStats.currentHealth);
        UpdateHeartCounters(playerInventory.regularHearts, playerInventory.gildedHearts);
    }

    public void UpdateWeaponUI(int ammoClipAmount)
    {
        Debug.Log("Updating UI");

        foreach (Transform child in ammoClipPos)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < ammoClipAmount; i++)
        {
            Instantiate(ammoClipImage, ammoClipPos);
        }

        GameObject weaponObject = GameObject.Find("WeaponSlot");

        if (weaponObject != null)
        {
            weaponSlot = weaponObject.GetComponent<Weapon>();
        }

        //ammoClipImageUI.texture = currentWeapon.CurrentWeapon.Data.ammoImage;
        weaponImage.texture = currentWeapon.CurrentWeapon.Data.weaponImage;
        ammoReserveText.text = currentWeapon.CurrentWeapon.AmmoReserve.ToString();
    }

    public void UpdateHealthUI(int currentHealth)
    {
        Debug.Log("Updated Health");

        hpText.text = currentHealth.ToString();

        if (currentHealth >= 100)
        {
            hpText.text = 100.ToString();
        }
        if (currentHealth <= 0)
        {
            hpText.text = 0.ToString();
        }
    }

    public void UpdateHeartCounters(int regHearts, int gildHearts)
    {
        regularHeartsText.text = regHearts.ToString();
        gildedHeartsText.text = gildHearts.ToString();
    }
}
