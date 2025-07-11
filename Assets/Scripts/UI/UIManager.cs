using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    #region
    public static UIManager instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    #endregion

    public UIPanle uIPanle;

    public void Init()
    {
        uIPanle = GetComponentInChildren<UIPanle>();
        uIPanle.Init();
    }
    void InitAndShowPanle(UIPanle _uiPanle)
    {
        _uiPanle.Init();
        _uiPanle.panlePopup.Show();
    }

    public void InitAndChangePanle(UIPanle _currentPanle, UIPanle _nextPanle)
    {
        _currentPanle.panlePopup.Hide();
        _nextPanle.Init();
        _nextPanle.panlePopup.Show();
    }
}
