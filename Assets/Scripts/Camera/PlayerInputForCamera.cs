using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInputForCamera : MonoBehaviour
{
    public Touch touch0;
    public Slider slider;
    public float touchCount;
    public bool rightInput;
    void Update()
    {
        HandlePlayerInput();
    }
    void HandlePlayerInput()
    {
        touchCount = Input.touchCount;
        if ( touchCount > 0)
        {
            touch0 = Input.GetTouch(0);
            RightInput();
        }
    }
    void RightInput()
    {
        if (touchCount == 1 && IsTouchOverUI(touch0.fingerId))
        {
            rightInput = true;
        }
        else
        {
            rightInput = false;
        }
    }
    public bool IsTouchOverUI(int fingerId)
    {
        return EventSystem.current.IsPointerOverGameObject(fingerId);
    }
}
