using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGamePanle : UIPanle
{

    public UIPanle mainMenuPanle;

    public override void Init()
    {
        base.Init();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        ChangePanle(this, mainMenuPanle);
    }
}
