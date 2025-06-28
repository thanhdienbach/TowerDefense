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
        GameStateMachine.Instance.ChangeState(GameStateMachine.Instance.gameOverState);
    }
    #endregion

    public UnitConfig mainHallConfig;
    public PlayingPanle playingPanle;
    public float energy;
    public void Init()
    {
        energy = 50;
        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        teamID = GetComponent<TargetFilterData>();
        myHealth.Init(mainHallConfig);
        teamID.Init(mainHallConfig);
        attack.Init();
    }

    public void SetEnergy(float value)
    {
        energy -= value;
    }
}
