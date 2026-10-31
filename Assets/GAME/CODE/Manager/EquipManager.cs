using UnityEngine;

/// <summary>
/// Handles the aspects of the equip menu in the shop screen (changing the order of the draggables affects
/// "Player_Inventory" and updates it
/// </summary>

public class EquipManager : MonoBehaviour
{
    public Player_Inventory playerInventory { get; private set; }

    public WeaponMod[] knifeSlots;
    public WeaponMod[] pistolSlots;
    public WeaponMod[] shotgunSlots;
    public WeaponMod[] reserveSlots;

    private void Awake()
    {
        playerInventory = GameObject.Find("Player").GetComponent<Player_Inventory>();
    }

    private void Start()
    {
        knifeSlots = playerInventory.knifeWeaponMods;
        pistolSlots = playerInventory.pistolWeaponMods;
        shotgunSlots = playerInventory.shotgunWeaponMods;
        reserveSlots = playerInventory.reserveWeaponMods;
    }

    public void UpdateMods()
    {
        knifeSlots = playerInventory.knifeWeaponMods;
        pistolSlots = playerInventory.pistolWeaponMods;
        shotgunSlots = playerInventory.shotgunWeaponMods;
        reserveSlots = playerInventory.reserveWeaponMods;
    }
}
