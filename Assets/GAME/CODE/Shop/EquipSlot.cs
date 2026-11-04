using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Empty slots that hold weapon mods on the Equip menu in Shops
/// </summary>

public class EquipSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        WeaponModEquipSlot draggableItem = dropped.GetComponent<WeaponModEquipSlot>();
        draggableItem.parentAfterDrag = transform;
    }
}
