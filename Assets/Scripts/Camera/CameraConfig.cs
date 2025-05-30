using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Camera/Config")]
public class CameraConfig : ScriptableObject
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
}
