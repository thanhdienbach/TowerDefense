using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    None,
    Player_MainHall,
    Player_EMP,
    Player_Lazer,
    Player_Turret,
    Player_Power,
    Enemy_Helicoptor,
    Enemy_Buggy,
    Enemy_Boss,
    All
}
public enum TeamId
{
    Player,
    Enemy,
    All
}
public enum TypeOfTarget
{
    TypeOfCompetitor,
    AllCompetitor,
    All
}
public enum ConditionTarget
{
    LowHP,
    Nearest,
}

