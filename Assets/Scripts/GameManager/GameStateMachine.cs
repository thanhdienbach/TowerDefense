using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{

    #region Instance
    public static GameStateMachine Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }
    #endregion

    public GameState currentState;
    void Start()
    {
        currentState = new MainMenuState();
        currentState.Enter();
    }

    
    void Update()
    {
        currentState.Update();
    }
    public void ChangeState(GameState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
