using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense.UI;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanle : UIPanle
{

    public Button backButton;
    public UIPanle mainMenuPanle;

    public Slider gameVolumeSlider;
    public TextMeshProUGUI gameVolumeText;

    public Slider fXVolumeSlider;
    public TextMeshProUGUI fXVolumeText;

    public CameraMainScene cameraMainScene;
    public Rotator testVolumeTurretRotator;
    public ShootBullet shootingBullet;

    public override void Init()
    {
        base.Init();

        backButton.onClick.AddListener(() => ChangePanle(this, mainMenuPanle));
        backButton.onClick.AddListener(SmothMoveToMainView);

        gameVolumeSlider.value = AudioManager.instance.gameVolume * 100;
        gameVolumeText.text = gameVolumeSlider.value.ToString("0");
        fXVolumeSlider.value = AudioManager.instance.fXVolume * 100;
        fXVolumeText.text = fXVolumeSlider.value.ToString("0");

        gameVolumeSlider.onValueChanged.AddListener(GameVolumeChange);
        fXVolumeSlider.onValueChanged.AddListener(FXVolumeChange);
    }


    public void GameVolumeChange(float value)
    {
        AudioManager.instance.audioSource.volume = value / 100;
        gameVolumeText.text = value.ToString("0");
    }

    public void FXVolumeChange(float value)
    {
        AudioManager.instance.fXVolume = value / 100;
        fXVolumeText.text = value.ToString("0");
    }

    public void SmothMoveToMainView()
    {
        shootingBullet.isShooting = false;
        testVolumeTurretRotator.isRotation = true;
        cameraMainScene.SmothMoveToMainView();
    }

}
