using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : UnitBase
{

    public UnitConfig towerConfig;

    void Start()
    {
        myHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
        teamID = GetComponent<TargetFilterData>();
        myHealth.Init(towerConfig);
        attack.Init();
        teamID.Init(towerConfig);
    }

}
