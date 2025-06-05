using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Animations;
using UnityEngine;


public enum ConditionTarget
{
    LowHP,
    Nearest,
    NearestMainHall,
}


public class Attack : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    public Weapon currentWeapon;

    public DeleyTimer findNewTargetTimer;
    public ConditionTarget conditionTarget;
    public LayerMask targetLayer;
    public float detectionRadius = 30;
    public Collider[] targetsInRange;
    public Transform bestTarget;

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

    

    

    void Start()
    {
        conditionTarget = ConditionTarget.Nearest;
    }


    void Update()
    {
        Attacking(currentWeapon);
    }
    public void Attacking(Weapon weapon)
    {
        if (findNewTargetTimer.IsReady())
        {
            FindTargetWithCondition(conditionTarget, targetsInRange);
            findNewTargetTimer.SetDeley(1);
        }
        if (bestTarget != null)
        {
            weapon.ShootBullet(bestTarget);
        }
    }
    void FindTargetWithCondition(ConditionTarget conditionTarget, Collider[] targetsInRange)
    {
        targetsInRange = Physics.OverlapSphere(transform.position, currentWeapon.weaponConfig.detectRange, targetLayer);
        if (targetsInRange.Length == 0)
        {
            bestTarget = null;
        }
        switch (conditionTarget)
        {
            case ConditionTarget.LowHP:
                FindLowHPTarGet(targetsInRange);
                break;
            case ConditionTarget.Nearest:
                FindNearestTarget(targetsInRange);
                break;
            case ConditionTarget.NearestMainHall:
                FindNearestMainHallTarget(targetsInRange);
                break;
        }
    }
    void FindLowHPTarGet(Collider[] targetsInRange)
    {
        float lowestHP = Mathf.Infinity;
        foreach (Collider targetInRange in targetsInRange)
        {
            float hp = targetInRange.GetComponent<Health>().curentHealth;
            if (hp < lowestHP)
            {
                lowestHP = hp;
                bestTarget = targetInRange.transform;
            }
        }
    }
    void FindNearestTarget(Collider[] targetsInRange)
    {
        float closestDistance = Mathf.Infinity;
        foreach (Collider targetInRange in targetsInRange)
        {
            float distance = Vector3.Distance(transform.position, targetInRange.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = targetInRange.transform;
            }
        }
    }
    void FindNearestMainHallTarget(Collider[] targetsInRange)
    {
        float closestDistance = Mathf.Infinity;
        foreach (Collider targetInRange in targetsInRange)
        {
            float distance = Vector3.Distance(MainHall.instance.transform.position, targetInRange.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = targetInRange.transform;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
