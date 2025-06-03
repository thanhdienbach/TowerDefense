using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponConfig weaponConfig;
    public ObjectBool bulletPool;
    public DeleyTimer timer;
    public float attackStep;
    public bool canAttack;
    public bool checkBullet;
    public float deleyTimer;

    private void Start()
    {
        bulletPool = GetComponent<ObjectBool>();
        bulletPool.PoolObject(weaponConfig.bullet, weaponConfig.amountOfBullet);
        attackStep = weaponConfig.attackStep;
    }
    public void ActiveBullet()
    {
        GameObject bullet =  bulletPool.GetPooledObject(bulletPool.pooledObject);
        if (timer.IsReady())
        {
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
            timer.SetDeley(weaponConfig.attackStep);
        }
    }
}
