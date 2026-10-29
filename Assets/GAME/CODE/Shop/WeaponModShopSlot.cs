using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class WeaponModShopSlot : MonoBehaviour
{
    [SerializeField] private RawImage weaponModImage;
    [SerializeField] private TextMeshProUGUI weaponModPrice;
    [SerializeField] private TextMeshProUGUI weaponModName;
    [SerializeField] private TextMeshProUGUI weaponModType;
    [SerializeField] private TextMeshProUGUI weaponModDescription;

    private WeaponMod weaponModToBuy;

    public void SetupShopSlot(WeaponMod mod)
    {
        weaponModImage.texture = mod.weaponModImage;
        weaponModPrice.text = mod.weaponModPrice.ToString();
        weaponModName.text = mod.weaponModName;
        weaponModType.text = mod.weaponType.ToString();
        weaponModDescription.text = mod.weaponModDescription;

        weaponModToBuy = mod;
    }

    public void ClickedOnWeaponMod()
    {
        ShopManager shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();

        shopManager.BuyWeaponMod(weaponModToBuy);
        Destroy(gameObject);
    }
}
