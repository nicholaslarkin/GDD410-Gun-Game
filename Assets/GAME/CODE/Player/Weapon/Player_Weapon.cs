using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private List<WeaponData> weapons = new();
    [SerializeField] private List<WeaponMod> weaponMods = new();

    private Player_Inventory inventory;

    private readonly List<Weapon> weaponInstances = new();
    private readonly List<WeaponMod> weaponModsInstances = new();

    public Weapon CurrentWeapon { get; private set; }

    private void Awake()
    {
        inventory = GetComponent<Player_Inventory>();

        inventory.InitializeReserveAmmo(weapons);

        foreach (WeaponData data in weapons)
        {
            if (data == null || data.weaponModel == null)
                continue;

            GameObject instance = Instantiate(data.weaponModel, weaponSlot);

            Weapon weapon = instance.GetComponent<Weapon>();

            if (weapon == null)
            {
                Debug.LogError(
                    $"{data.name} prefab is missing its Weapon component."
                );

                Destroy(instance);
                continue;
            }

            weapon.Initialize(data, inventory);
            instance.SetActive(false);

            weaponInstances.Add(weapon);
        }
    }

    private void Start()
    {
        WeaponSwap(3,0);
    }

    public Weapon GetWeapon(WeaponData data)
    {
        return weaponInstances.Find(weapon => weapon.Data == data);
    }

    //
    // TO DO: Finish up scroll wheel implementation
    //
    public void WeaponSwap(int weaponValue, int scrollWheelType) //UNFINISHED SCROLL WHEEL IMPLEMENTATION
    {
        int index = weaponValue - 1;

        if (index < 0 || index >= weaponInstances.Count)
        {
            Debug.LogWarning($"Invalid weapon selection: {weaponValue}");
            return;
        }

        if (CurrentWeapon != null)
            CurrentWeapon.gameObject.SetActive(false);

        if (scrollWheelType == 1)
        {
            index = (index - 1 + weaponInstances.Count)
                % weaponInstances.Count;
        }
        else if (scrollWheelType == 2)
        {
            index = (index + 1) % weaponInstances.Count;
        }

        CurrentWeapon = weaponInstances[index];
        CurrentWeapon.gameObject.SetActive(true);

        Player_Statistics.Instance.UpdateWeaponUI();
    }
}
