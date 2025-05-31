using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public PlayerInputForCamera playerInputForCamera;
    public CameraConfig cameraConfig;

    public float panSpeed = 5.0f; // Tốc độ kéo
    public float zoomSpeed = 0.5f; // Tốc độ zoom
    public float minZoom = 10f; // Giới hạn zoom gần
    public float maxZoom = 30f; // Giới hạn zoom xa
    public float newY;

    void Start()
    {
        playerInputForCamera = GetComponent<PlayerInputForCamera>();
    }

    void Update()
    {
        if (playerInputForCamera.touchCount == 1 && playerInputForCamera.touch0.position.x < Screen.width * 0.9) // Nếu chỉ có một điểm chạm trên màn hình và điển chạm không nằm trong 10% vùng bên phải màn hình
        {
            MoveCamera(panSpeed); // Di chuyển camera
        }
        ClampCamera();
        ZoomCamera();
    }

    /// <summary>
    /// Kéo camera theo hướng kéo ngón tay của người dùng theo trục x và z
    /// </summary>
    void MoveCamera(float moveSpeed)
    {
        if (playerInputForCamera.touch0.phase == TouchPhase.Moved) // Nếu giai đoạn của điển chạm đang là di chuyển
        {
            Vector2 delta = playerInputForCamera.touch0.deltaPosition; // Lấy thông tin sự thay đổi vị trí của điểm chạm so với frame trước
            Vector3 move = new Vector3(-delta.x * moveSpeed * Time.deltaTime, 0, -delta.y * moveSpeed * Time.deltaTime); // Tính toán điểm mà camera sẽ di chuyển đến
            transform.Translate(move, Space.World);
        }
    }
    void ClampCamera()
    {
        Vector3 clampPosition = transform.position;
        clampPosition.x = Mathf.Clamp(clampPosition.x, cameraConfig.minX, cameraConfig.maxX);
        clampPosition.z = Mathf.Clamp(clampPosition.z, cameraConfig.minZ, cameraConfig.maxZ);
        transform.position = clampPosition;
    }
    void ZoomCamera()
    {
        newY = ((maxZoom - minZoom) * playerInputForCamera.slider.value) + minZoom;
        transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
    }
}
