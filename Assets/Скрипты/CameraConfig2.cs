﻿using UnityEngine;
[CreateAssetMenu(menuName = "Camera/Config")]
public class CameraConfig2 : ScriptableObject
{
    public float turnSmooth;
    public float pivotSpeed;
    public float Y_rot_speed;
    public float X_rot_speed;
    public float minAngle;
    public float maxAngle;
    public float normalZ;
    public float normalX;
    public float aimZ;
    public float aimX;
    public float normalY;
}
