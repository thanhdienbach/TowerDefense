using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : GameState
{
    public override void Enter()
    {
        Time.timeScale = 0f;
    }
    public override void Update()
    {
        Debug.Log("MainMenu State");
    }
    public override void Exit()
    {

    }
}
