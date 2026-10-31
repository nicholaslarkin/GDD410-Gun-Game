using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Physical weapon mod that can be dragged around and placed into slots
/// Based on position "Player_Inventory" is updated to match its corresponding slot
/// </summary>

public class WeaponModEquipSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RawImage weaponModImage;
    [SerializeField] private TextMeshProUGUI weaponModName;
    [SerializeField] private TextMeshProUGUI weaponModDescription;

    private EquipManager equipManager;

    [HideInInspector] public Transform parentAfterDrag;

    private void Awake()
    {
        equipManager = GameObject.Find("EquipManager").GetComponent<EquipManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        weaponModImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        weaponModImage.raycastTarget = true;
        equipManager.UpdateMods();
    }

    /*public void SetupShopSlot(WeaponMod mod)
    {
        weaponModImage.texture = mod.weaponModImage;
        weaponModName.text = mod.weaponModName;
        weaponModDescription.text = mod.weaponModDescription;
    }*/
}
