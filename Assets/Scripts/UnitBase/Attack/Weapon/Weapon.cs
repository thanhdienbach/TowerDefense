using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponConfig weaponConfig;
    public ObjectBool bulletPool;
    public DeleyTimer nextTimeCanAttack;

    public TargetFilterData weaponFilterData;
    public Transform bulletSlot;


    private void Start()
    {
        bulletPool = GetComponent<ObjectBool>();
        weaponConfig.bullet.GetComponent<Bullet>().weaponConfig = weaponConfig;
        weaponFilterData = GetComponentInParent<TargetFilterData>();
        weaponConfig.bullet.GetComponent<Bullet>().bulletFilterData = weaponFilterData;
        bulletPool.PoolObject(weaponConfig.bullet, weaponConfig);
    }
    public void ShootBullet(Transform _bestTarget)
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
        bullet.transform.position = bulletSlot.position;
        bullet.transform.rotation = transform.rotation;
        if (weaponFilterData.unitType == UnitType.Player_Energy)
        {
            EnergyRecovery();
            return;
        }
        else if (weaponConfig.isNeedUpgradeTargetPosition)
        {
            bl.dynamicBestTarget = _bestTarget;
        }
        else
        {
            bl.staticBestTarget = _bestTarget;
        }
        bl.isMoving = true;
        bullet.SetActive(true);
    }
    void EnergyRecovery()
    {
        MainHall.instance.SetEnergy(weaponConfig.damage);
        PlayingPanle.instance.CheckCostOfTowerAndShowToUI();
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
