using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{

    #region instance
    public static SpawnEnemyController instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    #endregion

    /// <summary>
    /// Enemy list. It will be spawn in run time
    /// </summary>
    public List<Enemy> enemys = new List<Enemy>();
    public NumberOfEnemy numberOfEnemyConfig;
    public int curentWave;
    public bool showedCurentWave;
    public float numberOfCurrentEnemy;
    public float numberOfMaxEnemyCurrentWave;

    [Header("Random time to spawn variable")]
    public float firstDeleyEnemyTime = 2;
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

    [Header("Count destroy enemy")]
    public int destroyedEnemy;

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
        timer.SetDeley(firstDeleyEnemyTime);
        curentWave = 1;
        numberOfMaxEnemyCurrentWave = numberOfEnemyConfig.enemyInFirstWave;
    }

    void Update()
    {
        SpawnEnemyRoutine();
        ShowWaveInfoToTextUI();
    }
    void SpawnEnemyRoutine()
    {
        while (timer.IsReady() && !(curentWave == numberOfEnemyConfig.maxWave && numberOfCurrentEnemy == numberOfMaxEnemyCurrentWave))
        {
            randomTime = Random.Range(minTime, maxTime);
            timer.SetDeley(randomTime);
            Spawner();
        }
    }
    void Spawner()
    {
        if (numberOfCurrentEnemy < 2)
        {
            Instantiate(enemys[enemys.Count - 1], transform.position, transform.rotation);
            numberOfCurrentEnemy += 1;
        }
        else if (numberOfCurrentEnemy < numberOfEnemyConfig.enemyInFirstWave - 1)
        {
            RandomEnemy();
            Instantiate(enemys[indexOfEnemy], transform.position, transform.rotation);
            numberOfCurrentEnemy += 1;
        }
        else
        {
            Instantiate(enemys[0], transform.position, transform.rotation);
            curentWave += 1;
            // Add code to caculation numberOfMaxEnemyCurrentWave
            timer.SetDeley(float.MaxValue);
        }
        ShowCurrentWaveToSliderUI();
    }
    void ShowWaveInfoToTextUI()
    {
        if (!showedCurentWave)
        {
            PlayingPanle.instance.curentWaveInfo_Text.text = curentWave.ToString() + "/" + numberOfEnemyConfig.maxWave;
            showedCurentWave = !showedCurentWave;
        }
    }
    void ShowCurrentWaveToSliderUI()
    {
        PlayingPanle.instance.curentWaveProcessInfo_Slider.value = (numberOfCurrentEnemy / numberOfMaxEnemyCurrentWave);
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
    
    public void CountDestroyedEnemy()
    {
        destroyedEnemy += 1;
        if (destroyedEnemy == numberOfEnemyConfig.maxWave * (numberOfEnemyConfig.enemyInFirstWave + numberOfEnemyConfig.enemyInSecondWave + numberOfEnemyConfig.enemyInThirdWave))
        {
            GameStateMachine.Instance.ChangeState(GameStateMachine.Instance.winPlayScene);
        }
    }
}
