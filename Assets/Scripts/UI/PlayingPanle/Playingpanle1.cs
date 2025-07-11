using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playingpanle1 : UIPanle
{
    public UIPanle[] uIPanles;

    public override void Init()
    {
        base.Init();
        uIPanles = GetComponentsInChildren<UIPanle>(true);
        for (int i = 1; i < uIPanles.Length; i++)
        {
            uIPanles[i].Init();
        }
    }
}
