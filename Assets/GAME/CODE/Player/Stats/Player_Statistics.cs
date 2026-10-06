using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            float distance = hit.distance;
            float maxDistance = currentWeapon.maxRange;
            float exponent = 2f;

            float distancePercent = Mathf.Clamp01(distance / maxDistance);
            float falloff = Mathf.Pow(distancePercent, exponent);

            int damageCalc = Mathf.RoundToInt(
                Mathf.Lerp(currentWeapon.maxDamage, currentWeapon.minDamage, falloff));

            if (damageCalc <= 0)
            {
                Debug.Log("Damage too low! Counted as a miss!");
                return;
            }
            else
            {
                Debug.Log(damageCalc + " damage done when at a distance of " + hit.distance + " from target!");
                hit.collider.GetComponentInParent<ActorStatistics>().TakeDamage(damageCalc);
            }
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

    public override void Death()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("PrototypeMenu");
    }
}