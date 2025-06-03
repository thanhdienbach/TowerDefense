using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    public Weapon currentWeapon;

    public void Init(WeaponConfig weaponConfig)
    {
        LoadAllWeapon();
        EquipWeapon(weaponConfig);
    }
    void LoadAllWeapon()
    {
        Weapon[] loadedWeapon = Resources.LoadAll<Weapon>("Weapon");
        foreach (Weapon weapon in loadedWeapon)
        {
            weapons.Add(weapon);
        }
    }
    void EquipWeapon(WeaponConfig weaponConfig)
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }
    void ChangeWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    private void Update()
    {
         Attacking(currentWeapon);
    }
    void Attacking(Weapon weapon)
    {
        weapon.ActiveBullet();
    }
}
