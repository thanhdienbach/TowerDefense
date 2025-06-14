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
    public WeaponConfig weaponConfig;
    public PlayingPanle playingPanle;
    public float energy;
    public void Init()
    {
        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        myHealth.Init(mainHallConfig);
        attack.Init(weaponConfig);
    }  

}
