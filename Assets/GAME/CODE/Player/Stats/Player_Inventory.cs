using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Inventory : MonoBehaviour
{
    private Player_Motor playerMotor;
    private Player_Statistics playerStats;

    [Header("Mods")]
    public WeaponMod[] knifeWeaponMods = new WeaponMod[3];
    public WeaponMod[] pistolWeaponMods = new WeaponMod[3];
    public WeaponMod[] shotgunWeaponMods = new WeaponMod[3];
    public WeaponMod[] reserveWeaponMods = new WeaponMod[5];

    [Header("Stat Boosts")]
    [Range(0, 5)]
    public int healthStatBoosts;

    [Range(0, 5)]
    public int speedStatBoosts;

    [Range(0, 5)]
    public int jumpHeightBoosts;

    [Header("Hearts")]
    public int regularHearts;
    public int gildedHearts;

    private readonly Dictionary<WeaponData, int> reserveAmmo = new();

    private void Awake()
    {
        playerMotor = GetComponent<Player_Motor>();
        playerStats = GetComponent<Player_Statistics>();
    }

    //Built-in Unity Action that checks when scenes are loaded
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
        ApplyStatBoosts();
    }

    private void ApplyStatBoosts()
    {
        //Hard Coded Starting Values
        playerStats.maxHealth = 100 + (20 * healthStatBoosts);

        playerMotor.moveSpeed = 10 + (4 * speedStatBoosts);

        playerMotor.jumpForce = 10 + (2 * jumpHeightBoosts);
        playerMotor.fallForce = 10 + (2 * jumpHeightBoosts);
    }

    #region WEAPON MODS
    //Think of refactoring this to one later?
    public void AddKnifeMod(WeaponMod weaponMod)
    {
        if (weaponMod == null)
            return;

        if (!System.Array.Exists(knifeWeaponMods, mod => mod == weaponMod))
        {
            for (int i = 0; i < knifeWeaponMods.Length; i++)
            {
                if (knifeWeaponMods[i] == null)
                {
                    knifeWeaponMods[i] = weaponMod;
                    return;
                }
            }

            AddModToReserve(weaponMod);
        }
    }

    public void AddPistolMod(WeaponMod weaponMod)
    {
        if (weaponMod == null)
            return;

        if (!System.Array.Exists(pistolWeaponMods, mod => mod == weaponMod))
        {
            for (int i = 0; i < pistolWeaponMods.Length; i++)
            {
                if (pistolWeaponMods[i] == null)
                {
                    pistolWeaponMods[i] = weaponMod;
                    return;
                }
            }

            AddModToReserve(weaponMod);
        }
    }

    public void AddShotgunMod(WeaponMod weaponMod)
    {
        if (weaponMod == null)
            return;

        if (!System.Array.Exists(shotgunWeaponMods, mod => mod == weaponMod))
        {
            for (int i = 0; i < shotgunWeaponMods.Length; i++)
            {
                if (shotgunWeaponMods[i] == null)
                {
                    shotgunWeaponMods[i] = weaponMod;
                    return;
                }
            }

            AddModToReserve(weaponMod);
        }
    }

    public void AddModToReserve(WeaponMod weaponMod)
    {
        if (weaponMod == null)
            return;

        if (!System.Array.Exists(reserveWeaponMods, mod => mod == weaponMod))
        {
            for (int i = 0; i < reserveWeaponMods.Length; i++)
            {
                if (reserveWeaponMods[i] == null)
                {
                    reserveWeaponMods[i] = weaponMod;
                    return;
                }
            }
        }
    }
    #endregion



    #region RESERVE AMMO
    //
    // TO DO: Put this somewhere else later perhaps; only here because Inventory needs to read when ammo is bought from the store
    //
    public void InitializeReserveAmmo(List<WeaponData> weapons)
    {
        reserveAmmo.Clear();

        foreach (WeaponData data in weapons)
        {
            if (data == null || reserveAmmo.ContainsKey(data))
                continue;

            reserveAmmo.Add(data, data.ammoReserve);
        }
    }

    public int GetReserveAmmo(WeaponData data)
    {
        if (data != null && reserveAmmo.TryGetValue(data, out int amount))
            return amount;

        return 0;
    }

    public void AddReserveAmmo(WeaponData data, int amount)
    {
        if (data == null || amount <= 0)
            return;

        if (!reserveAmmo.ContainsKey(data))
            reserveAmmo.Add(data, 0);

        reserveAmmo[data] += amount;
    }

    public int ConsumeReserveAmmo(WeaponData data, int amount)
    {
        if (data == null || amount <= 0)
            return 0;

        int available = GetReserveAmmo(data);
        int consumed = Mathf.Min(available, amount);

        reserveAmmo[data] = available - consumed;

        return consumed;
    }
    #endregion
}
