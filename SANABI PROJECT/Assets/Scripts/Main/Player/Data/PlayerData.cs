using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class PlayerData : MonoBehaviour
{
    [Header("Run State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;

    [Header("In Air State")]
    public float variableJumpHeightMultiplier = 0.5f;
    public float addedForce = 10f;
    public float XVelocityLimit = 10f;

    [Header("WallJumpState")]
    public float wallJumpVelocity = 8f;
    public float wallJumpTime = 0.15f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("WallSlideState")]
    public float wallSlideVelocity = 11f;

    [Header("WallClimbState")]
    public float wallClimbVelocity = 6f;

    [Header("WallGrabState")]
    public float wallGrabOffSeconds = 0.7f;

    [Header("WireShootState")]
    public float wireLength = 10f;
    public float shootSpeed = 80f;

    [Header("WireGrappledState")]
    public float grappleAddedForce = 2f;
    public float DashCoolDown = 2f;
    public float DashForce = 25f;
    public float distanceBetweenImages = 0.1f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    private void Start()
    {
        whatIsGround = LayerMask.GetMask("NormalWall");
    }
}
