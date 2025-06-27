using System;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPanle : MonoBehaviour
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

    public Button eMP_Button;
    public TextMeshProUGUI eMP_Text;
    public Button laser_Button;
    public TextMeshProUGUI laser_Text;
    public Button machineGun_Button;
    public TextMeshProUGUI machineGun_Text;
    public Button powerl_Button;
    public TextMeshProUGUI powerl_Text;
    public List<Button> towersButton = new List<Button>();
    public tower emp_Tower;
    public tower lazer_Tower;
    public tower machineGun_Tower;
    public tower power_Tower;
    public StructureBuilder buildTower;
    public int indexOfButton;


    public void Init()
    {
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
    void ShowTowerCostToUI()
    {
        ShowInfoToUI(eMP_Text, emp_Tower.towerConfig.cost.ToString());
        ShowInfoToUI(laser_Text, lazer_Tower.towerConfig.cost.ToString());
        ShowInfoToUI(machineGun_Text, machineGun_Tower.towerConfig.cost.ToString());
        ShowInfoToUI(powerl_Text, power_Tower.towerConfig.cost.ToString());
    }
    public void ShowInfoToUI(TextMeshProUGUI textUI, string text)
    {
        textUI.text = text;
    }
    
    void Init_AddListener()
    {
        startWave_Button.onClick.AddListener(StartWaveButtonEvent);
        pause_Button.onClick.AddListener(PauseGame);
        Init_AddTowerButtonToListAndAddListener();
    }
    void Init_AddTowerButtonToListAndAddListener()
    {
        towersButton.Add(eMP_Button);
        towersButton.Add(laser_Button);
        towersButton.Add(machineGun_Button);
        towersButton.Add(powerl_Button);
        for (int i = 0; i < towersButton.Count; i++)
        {
            int index = i;
            towersButton[i].onClick.AddListener(() => SetTowerPrefabs(index));
        }
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
        GameStateMachine.Instance.ChangeState(GameStateMachine.Instance.playingState);
        isPlayingState = true;
    }
    void PauseGame()
    {
        if (isPlayingState)
        {
            GameStateMachine.Instance.ChangeState(GameStateMachine.Instance.pauseState);
            pause_Button.GetComponent<Image>().sprite = playImage;
        }
        else
        {
            GameStateMachine.Instance.ChangeState(GameStateMachine.Instance.playingState);
            pause_Button.GetComponent<Image>().sprite = pauseImage;
        }
        isPlayingState = !isPlayingState;
    }
    public void SetTowerPrefabs(int index)
    {
        buildTower.buildingPrefabs = buildTower.towers[index].GameObject();
    }

    private void Update()
    {
        CheckCostOfTowerAndShowToUI();
    }
    void CheckCostOfTowerAndShowToUI()
    {
        if (MainHall.instance.energy >= emp_Tower.towerConfig.cost)
        {
            eMP_Button.interactable = true;
        }
        else
        {
            eMP_Button.interactable = false;
        }
        if (MainHall.instance.energy >= lazer_Tower.towerConfig.cost)
        {
            laser_Button.interactable = true;
        }
        else
        {
            laser_Button.interactable = false;
        }
        if (MainHall.instance.energy >= machineGun_Tower.towerConfig.cost)
        {
            machineGun_Button.interactable = true;
        }
        else
        {
           machineGun_Button.interactable = false;
        }
        if (MainHall.instance.energy >= power_Tower.towerConfig.cost)
        {
            powerl_Button.interactable = true;
        }
        else
        {
            powerl_Button.interactable = false;
        }
        ShowInfoToUI(mainHallEnergy_Text, MainHall.instance.energy.ToString());
    }
}
