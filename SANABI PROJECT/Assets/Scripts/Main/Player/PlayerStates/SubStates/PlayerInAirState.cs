using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded; // to check if it's grounded
    private bool isTouchingWall;
    private float xInput;
    private bool isJumping; // to check if still jumping
    private bool jumpInputStop; // to check if finger off space bar
    public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks() 
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.Input.MovementInput.x;
        jumpInputStop = player.Input.JumpInputStop;

        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0f) // if xInput is in the direction of the wall
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);
            player.BodyAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.ArmAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);
        }
        

        FastFall();
        
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void FastFall()
    {
        if (player.CurrentVelocity.y < 0f)
        {
            player.playerRigidBody.gravityScale = 1.5f;
        }
        else
        {
            player.playerRigidBody.gravityScale = 1.0f;
        }
    }
    private void CheckJumpMultiplier()
    {
        if (isJumping) // if it's jumping
        {
            if (jumpInputStop) // if finger off the jump button
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier); // decrease the jump velocity(which is upwards)
                isJumping= false;
            }
            else if (player.CurrentVelocity.y <= 0f) // it's not jumping but rather falling
            {
                isJumping = false;
            }

        }
    }

    public void SetIsJumping() => isJumping = true;
}
