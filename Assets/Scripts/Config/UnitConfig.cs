using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Config/Unit")]
public class UnitConfig : ScriptableObject
{
    [Header("Parameter variable")]
    public float maxHP;
    public float cost;

    [Header("AI variable")]
    public float attackRange;
    public float moveSpeed;

    [Header("Other variable")]
    public float rarity;
    public TeamId team;
    public UnitType unitType;
    public TypeOfTarget typeOfTarget;
}
