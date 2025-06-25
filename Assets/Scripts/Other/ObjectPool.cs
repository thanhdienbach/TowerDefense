using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectBool : MonoBehaviour
{
    public List<GameObject> pooledObject;
    public List<Bullet> pooledBullet;
    public void PoolObject(GameObject objectToPool, WeaponConfig weaponConfig)
    {
        pooledObject = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < weaponConfig.amountOfBullet; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObject.Add(tmp);
        }
    }
    public GameObject GetPooledObject(List<GameObject> pooledObject)
    {
        for (int i = 0;i < pooledObject.Count;i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }
        return null;
    }
}
