using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullerMove : MonoBehaviour
{
    public DeleyTimer timer;
    void Update()
    {
        if (timer.IsReady())
        {
            gameObject.SetActive(false);
            timer.SetDeley(5);
        }
    }
}
