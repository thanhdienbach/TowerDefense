using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{

    #region instance
    public static MyGameManager instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    #endregion

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
