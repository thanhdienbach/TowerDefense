using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponConfig weaponConfig;
    public ObjectBool bulletPool;
    public DeleyTimer nextTimeCanAttack;

    public TargetFilterData weaponFilterData;

    private void Start()
    {
        bulletPool = GetComponent<ObjectBool>();
        weaponConfig.bullet.GetComponent<Bullet>().weaponConfig = weaponConfig;
        weaponFilterData = GetComponentInParent<TargetFilterData>();
        weaponConfig.bullet.GetComponent<Bullet>().bulletFilterData = weaponFilterData;
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
        bl.bestTarget = bestTarget;
        bl.isMoving = true;
    }
    private void OnDestroy()
    {
        for (int i = 0; i < bulletPool.pooledObject.Count; i++)
        {
            GameObject.Destroy(bulletPool.pooledObject[i]);
        }
        bulletPool.pooledObject = null;
    }
}
