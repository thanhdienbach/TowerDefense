using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponConfig weaponConfig;
    public ObjectBool bulletPool;
    public DeleyTimer nextTimeCanAttack;

    private void Start()
    {
        bulletPool = GetComponent<ObjectBool>();
        weaponConfig.bullet.GetComponent<Bullet>().weaponConfig = weaponConfig;
        bulletPool.PoolObject(weaponConfig.bullet, weaponConfig);
    }
    public void ShootBullet(Transform bestTarget)
    {
        if (!nextTimeCanAttack.IsReady())
        {
            return;
        }
        GameObject bullet = bulletPool.GetPooledObject(bulletPool.pooledObject);

        if (bullet == null)
        {
            return;
        }
        Bullet bl = bullet.GetComponent<Bullet>();
        if (bl == null)
        {
            return;
        }
        nextTimeCanAttack.SetDeley(weaponConfig.attackStep);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        bl.isMoving = true;
        bl.bestTarget = bestTarget;
    }
}
