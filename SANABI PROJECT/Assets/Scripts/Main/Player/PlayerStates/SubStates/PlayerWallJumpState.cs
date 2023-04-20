using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerController.SetWallJumpVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        playerController.CheckIfShouldFlip(wallJumpDirection);
        playerController.SetWallJumpEffectOn();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerController.BodyAnimator.SetFloat("yVelocity", playerController.CurrentVelocity.y);
        playerController.ArmAnimator.SetFloat("yVelocity", playerController.CurrentVelocity.y);

        if (startTime + playerData.wallJumpTime <= Time.time)
        {
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall) // if the player is touching the wall
        {
            // facing direction would be the wall
            wallJumpDirection = -playerController.FacingDirection; // so the wallJumpdirection should be the opposite of the facing direction
        }
        else
        {
            wallJumpDirection = +playerController.FacingDirection;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
