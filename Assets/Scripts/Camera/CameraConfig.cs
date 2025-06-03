using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Camera/Config")]
public class CameraConfig : ScriptableObject
{
    /// <summary>
    /// Clamp camera variable
    /// </summary>
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    /// <summary>
    /// Drag camera speed
    /// </summary>
    public float dragSpeed;

    /// <summary>
    /// Clamp zoom camera variable
    /// </summary>
    public float minZoom;
    public float maxZoom;
}
