using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public virtual void Enter()
    {

    }
    public virtual void Update()
    {
        Debug.Log("TestBase");
    }
    public virtual void Exit()
    {

    }
}
