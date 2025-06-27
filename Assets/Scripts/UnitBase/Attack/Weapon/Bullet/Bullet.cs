using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public DeleyTimer timer;
    public WeaponConfig weaponConfig;

    public bool isMoving;
    public Transform bestTarget;

    public float bulletSpeed = 20;
    public float raycashDistanceOfset = 1.5f;

    public TargetFilterData bulletFilterData;
    public Health targetHealth;
    public bool collided;

    public Transform lastPosition;

    private void OnEnable()
    {
        timer.SetDeley(weaponConfig.timeWillRecallBullet);
    }
    void Update()
    {
        RecallBullet();
        if (isMoving)
        {
            if (bestTarget == null)
            {
                // MoveBullet();
                gameObject.SetActive(false);
            }
            else
            {
                MoveBulletFollowBestTarget(bestTarget);
            }
        }
        CheckCollide();
        if (collided)
        {
            HandleCollide();
            collided = false;
        }
    }
    void RecallBullet()
    {
        if (timer.IsReady())
        {
            gameObject.transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
    public void MoveBulletFollowBestTarget(Transform bestTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, bestTarget.position, bulletSpeed * Time.deltaTime);
        transform.LookAt(bestTarget.position);
    }
    public void MoveBullet()
    {
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime);
        
    }
    void CheckCollide()
    {
        float distance = bulletSpeed * raycashDistanceOfset * Time.deltaTime;
        Vector3 direction = transform.forward;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, distance))
        {
            TargetFilterData competitorFilterData = hit.collider.gameObject.GetComponent<TargetFilterData>();
            if (competitorFilterData == null)
            {
                gameObject.transform.position = Vector3.zero;
                gameObject.SetActive(false);
                return;
            }
            targetHealth = hit.collider.gameObject.GetComponent<Health>();
            collided = competitorFilterData.teamId != bulletFilterData.teamId;
        }
    }
    void HandleCollide()
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.SetActive(false); 
        TakeDame(weaponConfig.damage);
        targetHealth.CheckCurrentHealth();
    }
    void TakeDame(float value)
    {
        targetHealth.curentHealth -= value;
    }
}
