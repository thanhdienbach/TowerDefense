using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : UnitBase
{
    [Header("Enemy parameter varriable")]
    public UnitConfig enemyConfig;
    public WeaponConfig weaponConfig;

    [Header("Enemy move variable")]
    public NavMeshAgent agent;
    public Transform destination;

    [Header("Other variable of enemy")]
    public float rarity;

    void Start()
    {
        teamID = enemyConfig.teamID;

        agent = GetComponent<NavMeshAgent>();
        destination = MainHall.instance.transform;
        EnemyAI();

        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        myHealth.Init(enemyConfig);
        attack.Init(weaponConfig);

        rarity = enemyConfig.rarity;
        agent.speed = enemyConfig.speed;
    }

    void FindTarget()
    {
        
    }

    public void EnemyAI() 
    {
        agent.SetDestination(destination.position);
    }
}
