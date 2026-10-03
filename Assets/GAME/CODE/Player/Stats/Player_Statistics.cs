using System;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class Player_Statistics : ActorStatistics
{
    public WeaponData currentWeapon;
    public Player_Weapon weapon;

    public int ammoCount;
    public int ammoReserve;

    protected override void Awake()
    {
        base.Awake();

        //hp = playerData.hp;
        //ammoCount = currentWeapon.ammoClip;
        //ammoReserve = currentWeapon.ammoReserve;
    }

    private void Start()
    {
        ammoCount = currentWeapon.ammoClip;
        ammoReserve = currentWeapon.ammoReserve;
    }

    public virtual void Shot(bool hurtboxDetected, RaycastHit hit)
    {
        ammoCount--;

        if (hurtboxDetected)
        {
            hit.collider.GetComponentInParent<ActorStatistics>().TakeDamage(currentWeapon.damage);
        }

        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.UpdateWeaponUI(ammoCount);
    }

    public void Reload()
    {
        int ammoMissing = currentWeapon.ammoClip - ammoCount;

        ammoReserve = ammoReserve - ammoMissing;
        ammoCount = currentWeapon.ammoClip;

        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.UpdateWeaponUI(ammoCount);
    }

    //USING MAGIC NUMBER HERE, CHANGE LATER TO DISPLAY NAMES FOR MORE CLARITy
    public void AddEffectFromPickup(int pickupArgument, int quantityAmount)
    {
        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        switch (pickupArgument)
        {
            //Health
            case 1:
                currentHealth += quantityAmount;
                uiManager.UpdateHealthUI(currentHealth);
                break;
            //Ammo
            case 2:
                ammoReserve += quantityAmount;
                uiManager.UpdateWeaponUI(ammoCount);
            break;
            //Misc
            case 3:
                Debug.Log("idk what to do with this misc pickup yet LOL");
                break;


        }
    }

    public void ChangeWeaponPrefab(WeaponData weapon)
    {
        currentWeapon = weapon;

        ammoCount = currentWeapon.ammoClip;
        ammoReserve = currentWeapon.ammoReserve;
    }
}