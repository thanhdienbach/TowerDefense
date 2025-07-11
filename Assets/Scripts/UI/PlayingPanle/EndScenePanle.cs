using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScenePanle : UIPanle
{
    public override void Init()
    {
        base.Init();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
