using System;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPanle : UIPanle
{
    #region instance
    public static PlayingPanle instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    #endregion

    public Button mainHallCurrentHealth_Button;
    public Button mainHallEnergy_Button;
    public TextMeshProUGUI mainHallCurrentHealth_Text;
    public TextMeshProUGUI mainHallEnergy_Text;


    public GameObject startWaveGameObject;
    public Button startWave_Button;

    public GameObject waveInfo_ScroolView;
    public TextMeshProUGUI curentWaveInfo_Text;
    public Slider curentWaveProcessInfo_Slider;

    public Button pause_Button;
    public bool isPlayingState;
    public Sprite playImage;
    public Sprite pauseImage;

    [Header("Builder tower buttons group")]
    public GameObject towerButtons_ScroolView; //ScroolView hold tower buttons
    public List<Button> towers_Button;
    public int indexOfButton;
    public List<TextMeshProUGUI> towersCost_Text;
    public StructureBuilder towersBuilder;

    public EndScenePanle endGamePanle;

    public override void Init()
    {
        base.Init();
        towersBuilder = StructureBuilder.instance;
        towers_Button = new List<Button>(towerButtons_ScroolView.GetComponentsInChildren<Button>());
        towersCost_Text = new List<TextMeshProUGUI>(towerButtons_ScroolView.GetComponentsInChildren<TextMeshProUGUI>());
        Init_AttributeUI();
        Init_ShowInforToUI();
        Init_AddListener();
    }
    void Init_AttributeUI()
    {
        mainHallCurrentHealth_Button.interactable = false;
        mainHallEnergy_Button.interactable = false;
        pause_Button.interactable = false;
    }
    void Init_ShowInforToUI()
    {
        ShowInfoToUI(mainHallCurrentHealth_Text, MainHall.instance.myHealth.curentHealth.ToString());
        ShowInfoToUI(mainHallEnergy_Text, MainHall.instance.energy.ToString());
        ShowTowerCostToUI();
    }

    /// <summary>
    /// At the same time addlisstener
    /// </summary>
    void ShowTowerCostToUI()
    {
        for (int i = 0; i < towers_Button.Count; i++)
        {
            ShowInfoToUI(towersCost_Text[i], towersBuilder.towers[i].towerConfig.cost.ToString());
            int index = i;
            towers_Button[i].onClick.AddListener(() => SetTowerPrefabs(index));
        }
    }
    public void ShowInfoToUI(TextMeshProUGUI textUI, string text)
    {
        textUI.text = text;
    }
    
    void Init_AddListener()
    {
        startWave_Button.onClick.AddListener(StartWaveButtonEvent);
        pause_Button.onClick.AddListener(PauseGame);
    }
    public void SetProcessToUI(Slider slider, float value)
    {
        slider.value = value;
    }
    void StartWaveButtonEvent()
    {
        startWaveGameObject.SetActive(false);
        waveInfo_ScroolView.SetActive(true);
        pause_Button.interactable = true;
        MyGameManager.instance.gameStateMachine.ChangeState(MyGameManager.instance.gameStateMachine.playingState);
        isPlayingState = true;
    }
    void PauseGame()
    {
        if (isPlayingState)
        {
            MyGameManager.instance.gameStateMachine.ChangeState(MyGameManager.instance.gameStateMachine.pauseState);
            pause_Button.GetComponent<Image>().sprite = playImage;
        }
        else
        {
            MyGameManager.instance.gameStateMachine.ChangeState(MyGameManager.instance.gameStateMachine.playingState);
            pause_Button.GetComponent<Image>().sprite = pauseImage;
        }
        isPlayingState = !isPlayingState;
    }
    public void SetTowerPrefabs(int index)
    {
        towersBuilder.SetBuilingPrefabsToBuild(index);
    }

    public void CheckCostOfTowerAndShowToUI()
    {
        float energy = MainHall.instance.energy;
        for (int i = 0; i < towers_Button.Count; i++)
        {
            if (energy >= towersBuilder.towers[i].towerConfig.cost)
            {
                towers_Button[i].interactable = true;
            }
            else
            {
                towers_Button[i].interactable = false;
            }
        }
        ShowInfoToUI(mainHallEnergy_Text, MainHall.instance.energy.ToString());
    }

    public void ChangeToEndGamePanle()
    {
        ChangePanle(this, endGamePanle);
    }
}
