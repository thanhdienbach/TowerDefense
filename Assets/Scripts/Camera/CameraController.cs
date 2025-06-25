using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public PlayerInputForCamera playerInputForCamera;
    public CameraConfig cameraConfig;
    public Vector2 delta;

    void Start()
    {
        playerInputForCamera = GetComponent<PlayerInputForCamera>();
    }

    void Update()
    {
        if (playerInputForCamera.rightInput)
        {
            MoveCamera(cameraConfig.dragSpeed);
        }
        ClampCamera();
        ZoomCamera();
    }

    /// <summary>
    /// Drag camera in x and z axis (Top down view)
    /// </summary>
    void MoveCamera(float moveSpeed)
    {
        // Get information of touch point compared last frame
        delta = playerInputForCamera.touch0.deltaPosition;
        // Caculate the point camera will move to
        Vector3 move = new Vector3(-delta.x * moveSpeed * Time.deltaTime, 0, -delta.y * moveSpeed * Time.deltaTime);
        // Move camera to caculated point
        transform.Translate(move, Space.World);
    }

    /// <summary>
    /// Clamp camera with x and y axis
    /// </summary>
    void ClampCamera()
    {
        Vector3 clampPosition = transform.position;
        clampPosition.x = Mathf.Clamp(clampPosition.x, cameraConfig.minX, cameraConfig.maxX);
        clampPosition.z = Mathf.Clamp(clampPosition.z, cameraConfig.minZ, cameraConfig.maxZ);
        transform.position = clampPosition;
    }

    /// <summary>
    /// Move up and down camera with slider
    /// </summary>
    void ZoomCamera()
    {
        float newY = ((cameraConfig.maxZoom - cameraConfig.minZoom) * playerInputForCamera.slider.value) + cameraConfig.minZoom;
        transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
    }
}
