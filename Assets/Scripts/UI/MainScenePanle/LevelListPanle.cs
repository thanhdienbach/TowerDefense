using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelListPanle : UIPanle
{

    public Button backButton;
    public UIPanle mainMenuPanle;
    public UIPanle playingPanle;

    public Button playButton;
    public List<Button> levelbuttons;
    public int sceneIndex;


    private void Awake()
    {
        backButton.onClick.AddListener(() => ChangePanle(this, mainMenuPanle));
        for (int i = 0; i < levelbuttons.Count; i++)
        {
            int index = i + 1;
            levelbuttons[i].onClick.AddListener(() => SetSceneIndex(index));
        }
        playButton.onClick.AddListener(OpenPlayScene);
    }

    public override void Init()
    {
        base.Init();
    }

    void SetSceneIndex(int _sceneIndex)
    {
        sceneIndex = _sceneIndex;
    }

    void OpenPlayScene()
    {
        switch (sceneIndex)
        {
            case 1:
                SceneManager.LoadScene("Scene1");
                break;
            case 2:
                Debug.Log("Open scene 2");
                break;
            case 3:
                Debug.Log("Open scene 3");
                break;
            case 4:
                Debug.Log("Open scene 4");
                break;
        }
    }

    private void OnDisable()
    {
        sceneIndex = -1;
    }
}
