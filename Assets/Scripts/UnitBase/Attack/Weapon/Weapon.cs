using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponConfig weaponConfig;
    public ObjectBool bulletPool;
    public DeleyTimer timer;

    private void Start()
    {
        bulletPool = GetComponent<ObjectBool>();
        bulletPool.PoolObject(weaponConfig.bullet, weaponConfig.amountOfBullet);
    }
    public void ShootBullet(Transform bestTarget)
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
        Bullet bl = bullet.GetComponent<Bullet>();
        if (bl != null)
        {
            bl.isMoving = true;
            bl.bestTarget = bestTarget;
        }
    }
}
