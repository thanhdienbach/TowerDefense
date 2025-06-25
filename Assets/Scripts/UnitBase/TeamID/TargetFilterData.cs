using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFilterData : MonoBehaviour
{ 
    public TeamId teamId;
    public UnitType unitType;
    public void Init(UnitConfig unit)
    {
        teamId = unit.team;
        unitType = unit.unitType;
    }
}
