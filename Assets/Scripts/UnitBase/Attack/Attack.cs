using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Animations;
using UnityEngine;


public class Attack : MonoBehaviour
{
    
    public List<Weapon> weapons = new List<Weapon>();
    public Weapon[] currentWeapons;

    public TypeOfTarget typeOfTarget;
    public TargetFilterData targetFilterData;
    public TeamId targetToTeam;
    public UnitType targetToUnitType;
    public Transform focusPosition;

    public DeleyTimer findNewTargetTimer;
    public ConditionTarget conditionTarget;
    public LayerMask targetLayer;
    public float detectionRadius = 30;
    public Collider[] targetsInRange;

    public Transform bestTarget;
    public LookAtTarget lookAtTarget;

    public LineRenderer lazerLineRenderer;


    public void Init()
    {
        LoadAllWeapon();
        currentWeapons = GetComponentsInChildren<Weapon>();
        targetFilterData = GetComponentInChildren<TargetFilterData>();
        focusPosition = this.transform;
        lookAtTarget = GetComponentInChildren<LookAtTarget>();
    }
    void LoadAllWeapon()
    {
        Weapon[] loadedWeapon = Resources.LoadAll<Weapon>("Weapon");
        foreach (Weapon weapon in loadedWeapon)
        {
            weapons.Add(weapon);
        }
    }

    void Start()
    {
        conditionTarget = ConditionTarget.Nearest;
        if (targetFilterData.unitType == UnitType.Player_Lazer)
        {
            lazerLineRenderer = GetComponent<LineRenderer>();
            DrawnLazerLine();
        }
        else
        {
            lazerLineRenderer = null;
        }
        
    }

    void Update()
    {
        Attacking(currentWeapons);
    }
    public void Attacking(Weapon[] weapons)
    {
        if (findNewTargetTimer.IsReady())
        {
            FindTargetWithCondition(targetsInRange, conditionTarget);
            findNewTargetTimer.SetDeley(1);
        }
        if (bestTarget != null)
        {
            if (lookAtTarget != null)
            {
                lookAtTarget.LookToTarget(bestTarget);
            }

            foreach(var weapon in weapons)
            {
                weapon.ShootBullet(bestTarget);
            }
        }
        if (targetFilterData.unitType == UnitType.Player_Lazer && bestTarget != null)
        {
            DrawnLazerLine();
        }
        else if (targetFilterData.unitType == UnitType.Player_Lazer && bestTarget == null)
        {
            lazerLineRenderer.SetPosition(1, weapons[0].bulletSlot.position);
        }
    }
    void DrawnLazerLine()
    {
        Weapon weapon = GetComponentInChildren<Weapon>();

        lazerLineRenderer.startColor = Color.white;
        lazerLineRenderer.endColor = Random.ColorHSV();

        lazerLineRenderer.positionCount = 2;

        lazerLineRenderer.SetPosition(0, weapon.bulletSlot.position);
        if (bestTarget == null)
        {
            lazerLineRenderer.SetPosition(1, weapon.bulletSlot.position);
            return;
        }
        if (weapon.nextTimeCanAttack.nextTimeCanDoSomeThing < Time.time + 1)
        {
            lazerLineRenderer.SetPosition(1, bestTarget.position);
        }
        else
        {
            lazerLineRenderer.SetPosition(1, weapon.bulletSlot.position);
        }
    }
    void FindTargetWithCondition(Collider[] targetsInRange, ConditionTarget conditionTarget)
    {
        targetsInRange = Physics.OverlapSphere(transform.position, currentWeapons[0].weaponConfig.detectRange, targetLayer);
        if (targetsInRange.Length == 0)
        {
            bestTarget = null;
        }
        if (typeOfTarget == TypeOfTarget.TypeOfCompetitor)
        {
            SetTypeOfTargetByUnitType(UnitType.None);
            switch (conditionTarget)
            {
                case ConditionTarget.LowHP:
                    FindLowHPTarGetByUnitType(targetsInRange, targetToUnitType);
                    break;
                case ConditionTarget.Nearest:
                    FindNearestStructerTargetByUnitType(targetsInRange, targetToUnitType, focusPosition);
                    break;
            }
        }
        else
        {
            SetTypeOfTargetByTeam(typeOfTarget);
            switch (conditionTarget)
            {
                case ConditionTarget.LowHP:
                    FindLowHPTarGetByTeamID(targetsInRange, typeOfTarget);
                    break;
                case ConditionTarget.Nearest:
                    FindNearestStructerTargetByTeamID(targetsInRange, typeOfTarget, focusPosition);
                    break;
            }
        }
        
    }
    void FindLowHPTarGetByUnitType(Collider[] targetsInRange, UnitType unitType)
    {
        float lowestHP = Mathf.Infinity;
        foreach (Collider targetInRange in targetsInRange)
        {
            float hp = targetInRange.GetComponent<Health>().curentHealth;
            if (hp < lowestHP && targetInRange.GetComponent<TargetFilterData>().unitType == unitType)
            {
                lowestHP = hp;
                bestTarget = targetInRange.transform;
            }
        }
    }
    void FindLowHPTarGetByTeamID(Collider[] targetsInRange, TypeOfTarget typeOfTarget)
    {
        float lowestHP = Mathf.Infinity;
        foreach (Collider targetInRange in targetsInRange)
        {
            float hp = targetInRange.GetComponent<Health>().curentHealth;
            if (typeOfTarget == TypeOfTarget.All)
            {
                if (hp < lowestHP)
                {
                    lowestHP = hp;
                    bestTarget = targetInRange.transform;
                }
            }
            else if (typeOfTarget == TypeOfTarget.AllCompetitor)
            {
                if (hp < lowestHP && targetInRange.GetComponent<TargetFilterData>().teamId == targetToTeam)
                {
                    lowestHP = hp;
                    bestTarget = targetInRange.transform;
                }
            }
        }
    }

    void FindNearestStructerTargetByUnitType(Collider[] targetsInRange, UnitType targetUnitType, Transform transform)
    {
        float closestDistance = Mathf.Infinity;
        foreach (Collider targetInRange in targetsInRange)
        {
            float distance = Vector3.Distance(transform.position, targetInRange.transform.position);
            if (distance < closestDistance && targetInRange.GetComponent<TargetFilterData>().unitType == targetUnitType)
            {
                closestDistance = distance;
                bestTarget = targetInRange.transform;
            }
        }
    }
    void FindNearestStructerTargetByTeamID(Collider[] targetsInRange, TypeOfTarget typeOfTarget, Transform transform)
    {
        float closestDistance = Mathf.Infinity;
        foreach (Collider targetInRange in targetsInRange)
        {
            float distance = Vector3.Distance(transform.position, targetInRange.transform.position);
            if (typeOfTarget == TypeOfTarget.All)
            {
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestTarget = targetInRange.transform;
                }
            }
            else if (typeOfTarget == TypeOfTarget.AllCompetitor)
            {
                if (distance < closestDistance && targetInRange.GetComponent<TargetFilterData>().teamId == targetToTeam)
                {
                    closestDistance = distance;
                    bestTarget = targetInRange.transform;
                }
            }
        }
    }
    void SetTypeOfTargetByUnitType(UnitType unitType)
    {
         targetToUnitType = unitType;
    }
    void SetTypeOfTargetByTeam(TypeOfTarget typeOfTarget)
    {
        if (typeOfTarget == TypeOfTarget.All)
        {
            targetToTeam = TeamId.All;
        }
        else if (typeOfTarget == TypeOfTarget.AllCompetitor)
        {
            if (targetFilterData.teamId == TeamId.Player)
            {
                targetToTeam = TeamId.Enemy;
            }
            else if (targetFilterData.teamId == TeamId.Enemy)
            {
                targetToTeam = TeamId.Player;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
