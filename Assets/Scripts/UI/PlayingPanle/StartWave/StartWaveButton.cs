using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWaveButton : MonoBehaviour
{
    public Button button;
    public WaveInfo_ScroolViewPopup waveInfo_ScroolViewPopup;
    public StartWaveButtonPopup popup;
    public GameState playingState;

    void Start()
    {
        playingState = new PlayingState();
        popup = GetComponent<StartWaveButtonPopup>();
        button.onClick.AddListener(HideThisAndDoThreeAction);
    }
    void HideThisAndDoThreeAction()
    {
        popup.Hide();
        PlayingPanle.instance.waveInfo_ScroolView.GetComponent<WaveInfo_ScroolViewPopup>().Show();
        PlayingPanle.instance.pause_Button.interactable = true;
        GameStateMachine.Instance.ChangeState(playingState);
    }
}
