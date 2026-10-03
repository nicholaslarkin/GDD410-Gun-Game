using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class UIManager : MonoBehaviour
{
    public Player_Statistics playerStats;
    public Weapon weaponSlot;

    public RawImage weaponImage;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI ammoReserveText;

    public GameObject ammoClipImage;
    public Transform ammoClipPos;

    public int ammoReserve;

    private void Start()
    {
        int currentHp = playerStats.currentHealth;
        int ammoClipAmount = playerStats.ammoCount;

        UpdateWeaponUI(ammoClipAmount);
        UpdateHealthUI(currentHp);
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

        weaponImage.texture = playerStats.currentWeapon.weaponImage;
        ammoReserveText.text = playerStats.ammoReserve.ToString();
    }

    public void UpdateHealthUI(int currentHealth)
    {
        Debug.Log("Updated Health");

        hpText.text = currentHealth.ToString();
    }
}
