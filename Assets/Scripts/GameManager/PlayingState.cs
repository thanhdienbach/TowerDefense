using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : GameState
{
    public override void Enter()
    {
        Time.timeScale = 1.0f;
    }
    public override void Update()
    {
        Debug.Log("Plaing State");
    }
    public override void Exit()
    {

    }
}
