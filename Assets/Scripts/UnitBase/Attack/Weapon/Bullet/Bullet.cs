using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public DeleyTimer timer;
    public WeaponConfig weaponConfig;

    public bool isMoving;
    public Transform bestTarget;

    private void OnEnable()
    {
        timer.SetDeley(weaponConfig.timeWillRecallBullet);
    }
    void Update()
    {
        RecallBullet();
        if (isMoving)
        {
            MoveBullet(bestTarget);
        }
    }
    void RecallBullet()
    {
        if (timer.IsReady())
        {
            gameObject.SetActive(false);
        }
    }
    public void MoveBullet(Transform bestTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, bestTarget.position, 20 * Time.deltaTime);
        transform.LookAt(bestTarget.position);
    }
}
