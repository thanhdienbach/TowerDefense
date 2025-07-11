using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{

    #region Instance
    public static GameStateMachine instance;
    private void OnEnable()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public GameState mainMenuState;
    public GameState pauseState;
    public GameState playingState;
    public GameState gameOverState;
    public GameState winPlayScene;
    public GameState currentState;
    void Awake()
    {
        CreatValueState();
        currentState = mainMenuState;
        currentState.Enter();
    }
    void CreatValueState()
    {
        mainMenuState = new MainMenuState();
        pauseState = new PauseState();
        playingState = new PlayingState();
        gameOverState = new GameOverState();
        winPlayScene = new WinPlayScene();
    }
    void Update()
    {
        currentState.Update();
    }
    public void ChangeState(GameState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
