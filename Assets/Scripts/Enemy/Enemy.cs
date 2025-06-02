using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : UnitBase
{
    public UnitConfig enemyConfig;
    public NavMeshAgent agent;
    public Transform destination;
    public float rarity;
    public float distance;
    public DeleyTimer timer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = MainHall.instance.transform;
        EnemyAI();

        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        myHealth.Init(enemyConfig);
        attack.Init(enemyConfig);
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
