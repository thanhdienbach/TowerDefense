using TMPro;
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

    public Button startWave_Button;
    public GameObject waveInfo_ScroolView;
    public TextMeshProUGUI curentWaveInfo_Text;
    public Slider curentWaveProcessInfo_Slider;
    public Button pause_Button;

    public Button eMP_Button;
    public Button laser_Button;
    public Button machineGun_Button;
    public Button powerl_Button;
    public void Init()
    {
        Init_Attribute();
        Init_ShowInforToUI();
        Init_AddListener();
    }
    void Init_Attribute()
    {
        mainHallCurrentHealth_Button.interactable = false;
        mainHallEnergy_Button.interactable = false;
        pause_Button.interactable = false;
    }
    void Init_ShowInforToUI()
    {
        ShowInfoToUI(mainHallCurrentHealth_Text, MainHall.instance.myHealth.curentHealth.ToString());
        ShowInfoToUI(mainHallEnergy_Text, MainHall.instance.energy.ToString());
    }
    public void ShowInfoToUI(TextMeshProUGUI textUI, string text)
    {
        textUI.text = text;
    }
    public void SetProcessToUI(Slider slider, float value)
    {
        slider.value = value;
    }
    void Init_AddListener()
    {
        startWave_Button.onClick.AddListener(StartWaveButtonEvent);
    }
    void StartWaveButtonEvent()
    {

    }
}
