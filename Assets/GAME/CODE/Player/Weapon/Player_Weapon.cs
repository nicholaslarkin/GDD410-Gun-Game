using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private List<WeaponData> weapons = new();
    [SerializeField] private List<WeaponMod> weaponMods = new();

    private readonly List<Weapon> weaponInstances = new();
    private readonly List<WeaponMod> weaponModsInstances = new();

    public Weapon CurrentWeapon { get; private set; }

    private void Awake()
    {
        foreach (WeaponData data in weapons)
        {
            if (data == null || data.weaponModel == null)
                continue;

            GameObject instance = Instantiate(
                data.weaponModel,
                weaponSlot
            );

            Weapon weapon = instance.GetComponent<Weapon>();

            if (weapon == null)
            {
                Debug.LogError(
                    $"{data.name} prefab is missing its Weapon component."
                );

                Destroy(instance);
                continue;
            }

            weapon.Initialize(data);
            instance.SetActive(false);

            weaponInstances.Add(weapon);
        }
    }

    private void Start()
    {
        WeaponSwap(3);
    }

    public void WeaponSwap(int weaponValue)
    {
        int index = weaponValue - 1;

        if (index < 0 || index >= weaponInstances.Count)
        {
            Debug.LogWarning($"Invalid weapon selection: {weaponValue}");
            return;
        }

        if (CurrentWeapon != null)
            CurrentWeapon.gameObject.SetActive(false);

        CurrentWeapon = weaponInstances[index];
        CurrentWeapon.gameObject.SetActive(true);

        Player_Statistics.Instance.UpdateWeaponUI();
    }
}
