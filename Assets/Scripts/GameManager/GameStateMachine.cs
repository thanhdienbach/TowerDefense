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

    public GameState pauseState;
    public GameState playingState;
    public GameState currentState;
    void Start()
    {
        CreatValueState();
        currentState = pauseState;
        currentState.Enter();
    }
    void CreatValueState()
    {
        pauseState = new PauseState();
        playingState = new PlayingState();
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
