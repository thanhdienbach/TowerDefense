using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneConfig", menuName = "Config/Scene")]
public class NumberOfEnemy : ScriptableObject
{
    public int maxWave;
    public int enemyInFirstWave;
    public int enemyInSecondWave;
    public int enemyInThirdWave;
}
