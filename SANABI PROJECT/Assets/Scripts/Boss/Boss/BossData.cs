using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossData : MonoBehaviour
{
    [Header("Appear State")]
    [Range(10f, 20f)] public float appearSpeed = 10f;

    [Header("Idle State")]
    public float idleWaitTime = 5f;

    [Header("Aiming State")]
    public float rotateSpeed = 200f;
    public float aimWaitTime = 1.5f;

    [Header("AimLock State")]
    public float aimLockTime = 0.5f;

    [Header("Shoot State")]
    public float shootWaitTime = 1f;

    [Header("Cooldown State")]
    public float cooldownWaitTime = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.5f;
    public LayerMask whatIsGround;


    private void OnEnable()
    {
        GameManager.Instance.bossData = this;
    }
}
