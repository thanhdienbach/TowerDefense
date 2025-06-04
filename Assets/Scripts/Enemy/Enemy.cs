using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : UnitBase
{
    [Header("Enemy parameter varriable")]
    public UnitConfig enemyConfig;
    public WeaponConfig weaponConfig;

    [Header("Enemy move variable")]
    public NavMeshAgent agent;
    public Transform destination;
    public bool destinationSet = false;

    [Header("Other variable of enemy")]
    public float rarity;
    public Attack enemyAttack;
    public DeleyTimer timer;

    void Start()
    {
        teamID = enemyConfig.teamID;

        agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<Attack>();

        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        myHealth.Init(enemyConfig);
        attack.Init(weaponConfig);

        rarity = enemyConfig.rarity;
        agent.speed = enemyConfig.speed;
        timer.SetDeley(float.MaxValue);
    }
    void Update()
    {
        EnemyAI();
    }
    void EnemyAI()
    {
        SetDestinationOneTime(FinDestination());
        if (!agent.pathPending)
        {
            agent.isStopped = false;
            timer.SetDeley(0);
        }
        if (enemyConfig.attackRange > agent.remainingDistance & timer.IsReady())
        {
            agent.isStopped = true;
            enemyAttack.Attacking(enemyAttack.currentWeapon);
        }
    }
    void SetDestinationOneTime(Transform destination)
    {
        if (!destinationSet)
        {
            agent.SetDestination(destination.position);
            destinationSet = true;
            agent.isStopped = true;
        }
    }
    Transform FinDestination()
    {
        return MainHall.instance.transform;
    }
}
