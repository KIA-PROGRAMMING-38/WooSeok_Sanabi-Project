using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretData : MonoBehaviour
{
    [Header("Aiming State")]
    public float aimTime = 1.5f;
    [Range(5f, 10f)] public float minRandomAngle = 5f;
    [Range(10f, 20f)] public float maxRandomAngle = 10f;
    [Range(150f, 200f)] public float rotateSpeed = 150f;

    [Header("Shoot State")]
    [Range(0.01f, 0.1f)] public float shootGapTime = 0.05f;
    public int shotBulletMaxNumber = 15;

    [Header("Cooldown State")]
    [Range(0.5f, 1f)]public float cooldownTime = 0.5f;
}
