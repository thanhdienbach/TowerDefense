using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActiveBullet : MonoBehaviour
{
    public DeleyTimer timer;
    public float attackRate = 4;
    private void OnEnable()
    {
        timer.SetDeley(attackRate);
    }
    void Update()
    {
        if (timer.IsReady())
        {
            gameObject.SetActive(false);
        }
    }
}
