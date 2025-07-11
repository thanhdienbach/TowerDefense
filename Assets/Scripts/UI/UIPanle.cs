using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UIPanle : MonoBehaviour
{
    
    public PanlePopup panlePopup;

    public virtual void Init()
    {
        panlePopup = this.AddComponent<PanlePopup>();
    }

    public void ChangePanle(UIPanle _currentPanle, UIPanle _nextPanle)
    {
        _currentPanle.panlePopup.Hide();
        _nextPanle.panlePopup.Show();
    }


}
