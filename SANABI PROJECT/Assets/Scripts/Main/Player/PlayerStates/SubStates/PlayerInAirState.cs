using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded; // to check if it's grounded
    private bool isTouchingWall;
    private float xInput;
    private bool JumpInput;
    private bool isJumping; // to check if still jumping
    private bool jumpInputStop; // to check if finger off space bar
    private bool MouseInput;
    private bool isDamaged;

    public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = playerController.CheckIfGrounded();
        isTouchingWall = playerController.CheckIfTouchingWall();
        isDamaged = playerController.CheckIfDamaged();    

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

        xInput = playerController.Input.MovementInput.x;
        jumpInputStop = playerController.Input.JumpInputStop;
        JumpInput = playerController.Input.JumpInput;
        MouseInput = playerController.Input.MouseInput;
        

        CheckJumpMultiplier();

        if (isGrounded && playerController.CurrentVelocity.y < 0.01f) // if landed on ground
        {
            stateMachine.ChangeState(playerController.LandState);
        }

        else if (isTouchingWall && xInput == playerController.FacingDirection) // if xInput is in the direction of the wall
        {
            stateMachine.ChangeState(playerController.WallGrabState);
        }
        else if (MouseInput)
        {
            stateMachine.ChangeState(playerController.WireShootState);
        }
        else if (isDamaged)
        {
            stateMachine.ChangeState(playerController.DamagedState);
        }
        else
        {
            playerController.CheckIfShouldFlip(xInput);
            playerController.SetVelocityX(playerData.runVelocity * xInput);


            playerController.BodyAnimator.SetFloat("yVelocity", playerController.CurrentVelocity.y);
            playerController.ArmAnimator.SetFloat("yVelocity", playerController.CurrentVelocity.y);
        }

        FastFall();

    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void FastFall()
    {
        if (playerController.CurrentVelocity.y < 0f)
        {
            playerController.playerRigidBody.gravityScale = 1.5f;
        }
        else
        {
            playerController.playerRigidBody.gravityScale = 1.0f;
        }
    }
    private void CheckJumpMultiplier()
    {
        if (isJumping) // if it's jumping
        {
            if (jumpInputStop) // if finger off the jump button
            {
                playerController.SetVelocityY(playerController.CurrentVelocity.y * playerData.variableJumpHeightMultiplier); // decrease the jump velocity(which is upwards)
                isJumping = false;
            }
            else if (playerController.CurrentVelocity.y <= 0f) // it's not jumping but rather falling
            {
                isJumping = false;
            }

        }
    }

    public void SetIsJumping() => isJumping = true;
}
