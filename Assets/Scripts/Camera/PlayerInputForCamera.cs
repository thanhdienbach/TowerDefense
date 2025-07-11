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
    public bool isFreeZone;

    private void Start()
    {
        isFreeZone = true;
    }
    void Update()
    {
        HandlePlayerInput();
        if (touch0.phase == TouchPhase.Ended)
        {
            isFreeZone = true ;
        }
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
        if (!IsTouchOverUI(touch0.fingerId))
        {
            rightInput = false;
            return;
        }
        if (touchCount == 1 && isFreeZone)
        {
            rightInput = true;
        }
        else
        {
            rightInput = false;
        }
    }
    public bool IsTouchOverUI(int _fingerId)
    {
        return EventSystem.current.IsPointerOverGameObject(_fingerId);
    }
}
