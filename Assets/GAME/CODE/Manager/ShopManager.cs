using UnityEngine;

/// <summary>
/// Handles buying and adding things to "Player_Inventory" if and when the player clicks on them in the Shop menu
/// </summary>

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Player_Statistics playerStats;
    [SerializeField] private Player_Inventory playerInventory;
    [SerializeField] private Player_Weapon playerWeapon;

    [SerializeField] private WeaponData pistolData;
    [SerializeField] private WeaponData shotgunData;

    [SerializeField] private WeaponModShopSlot weaponShopSlotPrefab;

    [SerializeField] private WeaponMod[] weaponMods;

    [SerializeField] private Transform[] weaponModSlots;

    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<Player_Statistics>();
        playerInventory = GameObject.Find("Player").GetComponent<Player_Inventory>();
        playerWeapon = GameObject.Find("Player").GetComponent<Player_Weapon>();

        for (int i = 0; i < weaponModSlots.Length; i++)
        {
            int selectedWeaponMod = Random.Range(0, weaponMods.Length);

            WeaponModShopSlot weaponSlot = Instantiate(weaponShopSlotPrefab, weaponModSlots[i]);

            weaponSlot.SetupShopSlot(weaponMods[selectedWeaponMod]);
        }
    }

    public void BuyWeaponMod(WeaponMod boughtWeaponMod)
    {
        switch (boughtWeaponMod.weaponType)
        {
            case WeaponTypes.Knife:
                playerInventory.AddKnifeMod(boughtWeaponMod);
                break;
            case WeaponTypes.Pistol:
                playerInventory.AddPistolMod(boughtWeaponMod);
                break;
            case WeaponTypes.Shotgun:
                playerInventory.AddShotgunMod(boughtWeaponMod);
                break;
        }
    }

    //
    // TO DO: Ammo buying doesnt work properly, fix later
    //
    public void BuyingPistolAmmo()
    {
        //Weapon pistol = playerWeapon.GetWeapon(pistolData);

        playerInventory.AddReserveAmmo(pistolData, 30);
    }

    public void BuyingShotgunAmmo()
    {
        //Weapon shotgun = playerWeapon.GetWeapon(shotgunData);

        playerInventory.AddReserveAmmo(shotgunData, 30);
    }

    public void BuyingHealth()
    {
        if (playerStats.currentHealth == playerStats.maxHealth)
            return;

        playerStats.currentHealth += 10;
    }
}
