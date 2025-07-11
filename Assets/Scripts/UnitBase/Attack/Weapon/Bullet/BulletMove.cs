using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    DeleyTimer deleyTimer;
    private void OnEnable()
    {
        deleyTimer.SetDeley(2f);
    }
    void Update()
    {
        transform.Translate(transform.forward);
        if (deleyTimer.IsReady())
        {
            Destroy(gameObject);
        }
    }

}
