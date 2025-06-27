using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameState
{
    public virtual void Enter()
    {

    }
    public virtual void Update()
    {
        
    }
    public virtual void Exit()
    {

    }
}
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
public class PauseState : GameState
{
    public override void Enter()
    {
        Time.timeScale = 0f;
    }
    public override void Update()
    {
        Debug.Log("Pause State");
    }
    public override void Exit()
    {

    }
}
