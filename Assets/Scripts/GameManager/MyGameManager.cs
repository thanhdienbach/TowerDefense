using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{

    List<UnitBase> unitBases;

    public MainHall maiHall;
    public SpawnEnemyController spawnEnemy;
    public UIManager uIManager;

    void Awake()
    {
        InitializeGameObject();
    }

    void InitializeGameObject()
    {
        maiHall.Init();
        spawnEnemy.Init();
        uIManager.Init();
    }

}
