using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bullet;
    public DeleyTimer deleyTimer;
    public Transform bulletSlot;
    public bool isShooting;

    void Update()
    {
        if (isShooting)
        {
            ShootBl();
        }
    }

    public void ShootBl()
    {
        if (deleyTimer.IsReady())
        {
            Instantiate(bullet, bulletSlot.position, Quaternion.identity);
            bulletSlot.GetComponent<TestVolume>().PlayShootBulletSound();
            deleyTimer.SetDeley(0.5f);
        }
    }
}
