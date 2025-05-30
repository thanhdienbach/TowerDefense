using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : UnitBase
{
    public UnitConfig enemyConfig;
    public NavMeshAgent agent;
    public Transform destination;
    void Start()
    {
        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        myHealth.Init(enemyConfig);
        attack.Init(enemyConfig);
        agent = GetComponent<NavMeshAgent>();
        destination = MainHall.instance.transform;
    }

    
    void Update()
    {
        agent.SetDestination(destination.position);
    }
}
