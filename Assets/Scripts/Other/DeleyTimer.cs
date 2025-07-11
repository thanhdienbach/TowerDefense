using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DeleyTimer 
{
    public float nextTimeCanDoSomeThing;

    public bool IsReady()
    {
        return Time.time >= nextTimeCanDoSomeThing;
    }

    public void SetDeley(float deleySecond)
    {
        nextTimeCanDoSomeThing = Time.time + deleySecond;
    }
}
