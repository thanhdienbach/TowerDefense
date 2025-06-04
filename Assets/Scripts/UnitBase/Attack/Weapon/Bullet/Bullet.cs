using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public DeleyTimer timer;
    public WeaponConfig weaponConfig;

    private void OnEnable()
    {
        timer.SetDeley(weaponConfig.attackRate);
    }
    void Update()
    {
        if (timer.IsReady())
        {
            gameObject.SetActive(false);
        }
        transform.position = Vector3.MoveTowards(transform.position, MainHall.instance.transform.position, 20 * Time.deltaTime);
        transform.LookAt(MainHall.instance.transform.position);
    }
}
