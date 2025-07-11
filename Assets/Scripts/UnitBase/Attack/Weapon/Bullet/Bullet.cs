using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public DeleyTimer timer;
    public WeaponConfig weaponConfig;

    public bool isMoving;
    public Transform dynamicBestTarget;
    public Transform staticBestTarget;
    public Vector3 direction;
    public bool hasDirection;

    public float raycashDistanceOfset = 1.5f;

    public TargetFilterData bulletFilterData;
    public Health targetHealth;
    public bool collided;

    bool isMainHall;

    public BulletEffect bulletEffect;
    public bool playedBulletEffect;

    private void OnEnable()
    {
        timer.SetDeley(weaponConfig.timeWillRecallBullet);
        bulletEffect = GetComponent<BulletEffect>();
        playedBulletEffect = false;
        hasDirection = false;
    }
    void Update()
    {
        RecallBullet();
        
        if (isMoving)
        {
            if (dynamicBestTarget != null)
            {
                MoveBulletFollowBestTarget(dynamicBestTarget.position);
            }
            else if (staticBestTarget != null)
            {
                MoveBulletByDirection(staticBestTarget.position);
            }
            else
            {
                hasDirection = false;
                gameObject.SetActive(false);
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
            CallBullet();
        }
    }
    public void MoveBulletFollowBestTarget(Vector3 _dynamicBestTarget)
    {
        if (!playedBulletEffect)
        {
            bulletEffect.ShootBulletEffect();
            playedBulletEffect = true;
        }
        transform.LookAt(_dynamicBestTarget);
        transform.position = Vector3.MoveTowards(transform.position, _dynamicBestTarget, weaponConfig.bulletSpeed * Time.deltaTime);
        direction = (_dynamicBestTarget - transform.position).normalized;
    }
    public void MoveBulletByDirection(Vector3 _staticBestTarget)
    {
        if (!playedBulletEffect)
        {
            bulletEffect.ShootBulletEffect();
            playedBulletEffect = true;
        }
        if (!hasDirection)
        {
            staticBestTarget.position = new Vector3(staticBestTarget.position.x, staticBestTarget.position.y, staticBestTarget.position.z);
            direction = (_staticBestTarget - transform.position).normalized;
            hasDirection = true;
        }
        transform.position += direction * weaponConfig.bulletSpeed * Time.deltaTime;
    }
    void CheckCollide()
    {
        float distance = weaponConfig.bulletSpeed * raycashDistanceOfset * Time.deltaTime;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            TargetFilterData competitorFilterData = hit.collider.gameObject.GetComponent<TargetFilterData>();
            if (competitorFilterData == null)
            {
                CallBullet();
                return;
            }
            else if (competitorFilterData.unitType == UnitType.Player_MainHall && this.bulletFilterData.unitType != UnitType.Player_MainHall)
            {
                isMainHall = true;
            }
            targetHealth = hit.collider.gameObject.GetComponent<Health>();
            collided = competitorFilterData.teamId != bulletFilterData.teamId;
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.green);
        
    }
    void HandleCollide()
    {
        bulletEffect.BulletHitEffect(this.transform);
        TakeDame(weaponConfig.damage);
        if (targetHealth.curentHealth <= 0)
        {
            bulletEffect.DestroyEffect(this.transform);
        }
        CallBullet();
    }
    void CallBullet()
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }
    void TakeDame(float value)
    {
        targetHealth.curentHealth -= value;
        targetHealth.CheckCurrentHealth();
        if (isMainHall && targetHealth.curentHealth > 0)
        {
            PlayingPanle.instance.ShowInfoToUI(PlayingPanle.instance.mainHallCurrentHealth_Text, MainHall.instance.myHealth.curentHealth.ToString());
        }
    }
}
