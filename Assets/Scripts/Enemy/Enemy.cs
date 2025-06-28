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

    [Header("Enemy move variable")]
    public NavMeshAgent agent;
    public Transform destination;
    public bool destinationSet = false;

    [Header("Other variable of enemy")]
    public float rarity;
    public DeleyTimer timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        teamID = GetComponent<TargetFilterData>();
        myHealth.Init(enemyConfig);
        attack.Init();
        teamID.Init(enemyConfig);

        rarity = enemyConfig.rarity;
        agent.speed = enemyConfig.speed;
        timer.SetDeley(float.MaxValue);
    }
    void Update()
    {
        EnemyAI();
    }
    private void OnDisable()
    {
        SpawnEnemyController.instance.CountDestroyedEnemy();
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
            attack.Attacking(attack.currentWeapon);
            transform.LookAt(destination);
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
