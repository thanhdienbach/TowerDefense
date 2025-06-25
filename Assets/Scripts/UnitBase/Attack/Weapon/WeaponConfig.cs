using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weaponconfig", menuName = "Config/Weapon")]
public class WeaponConfig : ScriptableObject
{
    public float damage;
    public int amountOfBullet;
    public GameObject bullet;
    public float detectRange;
    public float attackStep;
    public float timeWillRecallBullet;
}
