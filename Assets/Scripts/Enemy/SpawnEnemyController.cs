using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{
    /// <summary>
    /// Enemy list. It will be spawn in run time
    /// </summary>
    public List<Enemy> enemys = new List<Enemy>();

    [Header("Random time to spawn variable")]
    public float minTime = 2;
    public float maxTime = 10;
    public float randomTime;
    public DeleyTimer timer;

    [Header("Random enemy tospawn variable")]
    public float totalRarity;
    public List<float> rarityList;
    float rarityCumulative;
    public int indexOfEnemy;
    public float randomNumber;

    public void Init()
    {
        Enemy[] loadedEnemy = Resources.LoadAll<Enemy>("Enemy");
        foreach (Enemy enemy in loadedEnemy)
        {
            totalRarity += enemy.rarity;
            enemys.Add(enemy);
        }
        enemys.Sort((a,b) => a.rarity.CompareTo(b.rarity));
        for (int i = 0; i < enemys.Count; i++)
        {
            rarityCumulative += enemys[i].rarity;
            rarityList.Add(rarityCumulative);
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
            randomTime = Random.Range(minTime, maxTime);
            timer.SetDeley(randomTime);
            Spawner();
        }
    }
    void Spawner()
    {
        Instantiate(enemys[RandomEnemy()], transform.position, transform.rotation); // Thêm code để random enemy
    }
    int RandomEnemy()
    {
        randomNumber = Random.Range(1, rarityList.Max());
        for (int i = 0;i < rarityList.Count;i++)
        {
            if (randomNumber <= rarityList[i])
            {
                indexOfEnemy = i;
                break;
            }
            
        }
        return indexOfEnemy;
    }
}
