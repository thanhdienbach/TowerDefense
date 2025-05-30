using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{
    /// <summary>
    /// Enemy list. It will be spawn in run time
    /// </summary>
    public List<Enemy> enemys = new List<Enemy>();

    public float minTime = 2;
    public float maxTime = 5;
    public float randomTime;
    public DeleyTimer timer;

    public void Init()
    {
        Enemy[] loadedEnemy = Resources.LoadAll<Enemy>("Enemy");
        foreach (Enemy enemy in loadedEnemy)
        {
            enemys.Add(enemy);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        SpawnEnemyRoutine();
    }
    void SpawnEnemyRoutine()
    {
        while (timer.IsReady())
        {
            Spawner();
            randomTime = Random.Range(minTime, maxTime);
            timer.SetDeley(randomTime);
        }
    }
    void Spawner()
    {
        Instantiate(enemys[1], transform.position, transform.rotation); // Thêm code để random enemy
    }
}
