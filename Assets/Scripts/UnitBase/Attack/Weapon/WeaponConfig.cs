using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weaponconfig", menuName = "Config/Weapon")]
public class WeaponConfig : ScriptableObject
{
    public string Name;
    public WeaponId weaponID;
    public float damage;
    public int amountOfBullet;
    public GameObject bullet;
    public float attackRate;
    public float attackStep;
}
