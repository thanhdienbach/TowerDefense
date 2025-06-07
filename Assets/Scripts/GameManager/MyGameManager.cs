using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{

    public MainHall maiHall;
    public SpawnEnemyController spawnEnemy;
    public UIManager uIManager;
    
    void Awake()
    {
        InitializeGameObject();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void InitializeGameObject()
    {
        maiHall.Init();
        spawnEnemy.Init();
        uIManager.Init();
    }
}
