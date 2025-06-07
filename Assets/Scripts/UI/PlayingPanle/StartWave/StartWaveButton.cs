using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWaveButton : MonoBehaviour
{
    public Button button;
    public WaveInfo_ScroolViewPopup waveInfo_ScroolViewPopup;
    public StartWaveButtonPopup popup;

    void Start()
    {
        popup = GetComponent<StartWaveButtonPopup>();
        button.onClick.AddListener(HideThisAndDoTwoAction);
    }
    void HideThisAndDoTwoAction()
    {
        popup.Hide();
        PlayingPanle.instance.waveInfo_ScroolView.GetComponent<WaveInfo_ScroolViewPopup>().Show();
        PlayingPanle.instance.pause_Button.interactable = true;
    }
}
