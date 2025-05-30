using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputForCamera : MonoBehaviour
{
    public Touch touch0;
    public byte touchCount;
    public Slider slider;
    public float touchX;
    public float touchY;

    void Update()
    {
        HandlePlayerInput();
        touchX = touch0.position.x;
        touchY = touch0.position.y;
    }
    void HandlePlayerInput()
    {
        touchCount = (byte)Input.touchCount;
        if (touchCount == 1)
        {
            touch0 = Input.GetTouch(0);
        }
    }
}
