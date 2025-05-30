using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Config/Unit")]
public class UnitConfig : ScriptableObject
{
    public float maxHP;
    public float damage;
    public float speed;
}
