using System.Collections;
using System.Collections.Generic;
using TowerDefense.UI;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanle : UIPanle
{

    public Button levelSelecButton;
    public Button optionButton;
    public Button quitGameButton;

    public UIPanle mainMenuPanle;
    public UIPanle levelListPanle;
    public UIPanle optionListPanle;
    public UIPanle quitGamePanle;

    public CameraMainScene cameraMainScene;
    public Rotator testVolumeTurretRotator;
    public ShootBullet shootingBullet;

    public override void Init()
    {
        base.Init();
        levelSelecButton.onClick.AddListener(ShowLevelListPanle);
        optionButton.onClick.AddListener(ShowOptionPanle);
        quitGameButton.onClick.AddListener(ShowQuitGamePanle);
    }

    void ShowLevelListPanle()
    {
        ChangePanle(mainMenuPanle, levelListPanle);
    }
    void ShowOptionPanle()
    {
        testVolumeTurretRotator.isRotation = false;
        shootingBullet.isShooting = true;
        cameraMainScene.SmothMoveToTurret();
        ChangePanle(mainMenuPanle, optionListPanle);
    }
    void ShowQuitGamePanle()
    {
        ChangePanle(mainMenuPanle, quitGamePanle);
    }

}
