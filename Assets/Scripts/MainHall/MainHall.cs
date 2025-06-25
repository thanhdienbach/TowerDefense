using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHall : UnitBase
{

    #region instance
    public static MainHall instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    #endregion

    public UnitConfig mainHallConfig;
    public PlayingPanle playingPanle;
    public float energy;
    public void Init()
    {
        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        teamID = GetComponent<TargetFilterData>();
        myHealth.Init(mainHallConfig);
        teamID.Init(mainHallConfig);
        attack.Init();
    }  

}
