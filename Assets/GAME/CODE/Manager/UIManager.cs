using TMPro;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class UIManager : MonoBehaviour
{
    public Player_Stats playerStats;
    public Weapon weaponSlot;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI ammoReserveText;

    public GameObject ammoClipImage;
    public Transform ammoClipPos;

    public int hp;
    public int ammoCount;
    public int ammoReserve;

    private void Update()
    {
        UpdateUIInfo();
        //UpdateAmmoClip();
    }

    public void UpdateUIInfo()
    {
        GameObject weaponObject = GameObject.Find("WeaponSlot");

        if (weaponObject != null)
        {
            weaponSlot = weaponObject.GetComponent<Weapon>();
        }

        hp = playerStats.hp;
        ammoCount = playerStats.ammoCount;
        ammoReserve = playerStats.ammoReserve;

        hpText.text = hp.ToString();
        ammoReserveText.text = ammoReserve.ToString();
    }

    /*public void UpdateAmmoClip()
    {
        for (int i = 0; i <= ammoCount; i++)
        {
            ammoClipImage = Instantiate(ammoClipImage, ammoClipPos);
        }

        Destroy(ammoClipImage);
    }*/
}
