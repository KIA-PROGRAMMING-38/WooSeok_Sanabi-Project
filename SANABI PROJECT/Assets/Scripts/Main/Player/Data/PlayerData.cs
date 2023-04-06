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

    [Header("WallJumpState")]
    public float wallJumpVelocity = 8f;
    public float wallJumpTime = 0.1f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("WallSlideState")]
    public float wallSlideVelocity = 3f;

    [Header("WallClimbState")]
    public float wallClimbVelocity = 2f;

    [Header("WallGrabState")]
    public float wallGrabOffSeconds = 0.7f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    private void Start()
    {
        whatIsGround = LayerMask.GetMask("NormalWall");
    }
}
