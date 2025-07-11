using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{

    #region instance
    public static MyGameManager instance;
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

    public List<UnitBase> unitBases;

    public string sceneName;
    public GameStateMachine gameStateMachine;

    private void Awake()
    {
        
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        sceneName = SceneManager.GetActiveScene().name;
        InitializeGameObject(sceneName);
    }

    public void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        Debug.Log("LoadScene");
        sceneName = _scene.name;

        gameStateMachine = GetComponent<GameStateMachine>();

        if (_scene.name == "MainMenuScene") // Thay bằng scene index
        {
            gameStateMachine.ChangeState(gameStateMachine.mainMenuState);
        }
        else
        {
            gameStateMachine.ChangeState(gameStateMachine.pauseState);
        }

        InitializeGameObject(_scene.name);
    }

    void InitializeGameObject(string sceneName)
    {
        AudioManager.instance.Init(sceneName);
        
        if (sceneName != "MainMenuScene")
        {
            MainHall.instance.Init();
            SpawnEnemyController.instance.Init();
        }

        UIManager.instance.Init();
    }

}
