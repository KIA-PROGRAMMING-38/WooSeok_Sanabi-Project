using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class PlayerData : MonoBehaviour
{
    [Header("PlayerStatus")]
    public int playerHP = 4;
    public int PlayerTakeDamage = 1;
    public int PlayerHPRecoverStrength = 1;
    public float playerMinVelocityY = -18f;
    public float playerMaxVelocityY = 15f;    
    [Range(0.01f, 0.05f)] public float afterImageGapTime = 0.05f;

    [Header("Walk State")]
    public float walkVelocity = 2f;

    [Header("Run State")]
    public float runVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;

    [Header("In Air State")]
    [Range(0f, 1f)]public float variableJumpHeightMultiplier = 0.5f;
    public float addedForce = 10f;
    public float XVelocityLimit = 10f;

    [Header("WallJump State")]
    public float wallJumpVelocity = 8f;
    [Range(0f, 0.5f)] public float wallJumpTime = 0.15f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("WallSlide State")]
    public float wallSlideVelocity = 11f;

    [Header("WallClimb State")]
    public float wallClimbVelocity = 6f;

    [Header("WallGrab State")]
    [Range(0f, 1f)] public float wallGrabOffSeconds = 0.7f;

    [Header("WireShoot State")]
    public float wireLength = 10f;
    public float shootSpeed = 110f;
    

    [Header("WireGrappled State")]
    public float grappleAddedForce = 2f;

    [Header("Damaged State")]
    public float damageResetTime = 8f;
    public float damagedJumpVelocity = 8f;
    [Range(0f, 1f)] public float damagedOutTime = 0.5f;
    [Range(0f, 10f)] public float invincibleTime = 1f;
    [Range(0f, 0.5f)] public float damagedSlowTime = 0.05f;
    [Range(0f, 1f)] public float damagedTimeScale = 0.2f;

    [Header("SwingDash")]
    public float DashCoolDown = 2f;
    public float DashForce = 25f;
    [Range(0f, 1f)] public float DashTime = 0.3f;

    [Header("DamagedDash")]
    public float damagedDashVelocity = 15f;

    [Header("ApproachDash")]
    [Range(50f, 100f)] public float approachDashVelocity = 70f;

    [Header("ExecuteHold")]
    public float executeHoldMaxTime = 5f;
    [Range(0f, 0.5f)] public float holdedSlowTime = 0.05f;
    [Range(0f, 1f)] public float holdedTimeScale = 0.1f;

    [Header("ExecuteDash")]
    public float executeDashVelocity = 20f;
    [Range(0f, 0.5f)] public float executeDashSlowTime = 0.005f;
    [Range(0f, 1f)] public float executeDashTimeScale = 0.05f;

    [Header("Menace")]
    public float menaceWaitTime = 3f;

    [Header("FinishBoss")]
    [Range(0f, 1.5f)] public float finishBossSlowTime = 1f;
    [Range(0f, 1f)] public float finishBossTimeScale = 0.5f;


    [Header("Check Variables")]
    [Range(0f, 0.3f)] public float groundCheckRadius = 0.1f;
    [Range(0f, 0.7f)] public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    private void Start()
    {
        whatIsGround = LayerMask.GetMask("NormalWall");
    }
}
