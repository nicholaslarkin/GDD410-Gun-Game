using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public Player_Stats stats;
    public UIManager uiManager;
    public Transform weaponSlot;
    public GameObject currentWeapon;

    public List<WeaponData> weapons = new List<WeaponData>();

    private void Awake()
    {
        Debug.Log(weapons.Count);
    }

    private void Start()
    {
        WeaponSwap(3);
    }

    public void WeaponSwitching(int weaponValue)
    {
        switch (weaponValue)
        {
            case 1:
                WeaponSwap(weaponValue);
                break;

            case 2:
                WeaponSwap(weaponValue);
                break;

            case 3:
                WeaponSwap(weaponValue);
                break;

            default:
                break;
        }
    }

    public void WeaponSwap(int weaponValue)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        WeaponData weaponData = weapons[weaponValue - 1];

        currentWeapon = Instantiate(weapons[weaponValue - 1].weaponModel, weaponSlot);

        stats.ChangeWeaponPrefab(weaponData);
        uiManager.UpdateUIInfo();
        //uiManager.UpdateAmmoClip();

        Debug.Log("Switched to " + weapons[weaponValue - 1].name);
    }
}
