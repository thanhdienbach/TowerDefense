using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public GameState currentState;
    void Start()
    {
        currentState = new MainMenuState();
        currentState.Enter();
    }

    
    void Update()
    {
        // currentState.Update();
    }
    public void ChangeState(GameState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
