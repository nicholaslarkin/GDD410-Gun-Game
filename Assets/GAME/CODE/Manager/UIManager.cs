using TMPro;
using UnityEngine;

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

    private void Awake()
    {
        hp = playerStats.hp;
        ammoCount = weaponSlot.ammoClip;
        ammoReserve = weaponSlot.ammoReserve;

        for (int i = 0; i <= ammoCount; i++)
        {
            ammoClipImage = Instantiate(ammoClipImage, ammoClipPos);
        }

        Destroy(ammoClipImage);
    }

    private void Update()
    {
        weaponSlot = GetComponentInChildren<Weapon>();

        hpText.text = hp.ToString();
        ammoReserveText.text = ammoReserve.ToString();
    }
}
