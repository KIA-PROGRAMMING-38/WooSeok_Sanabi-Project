using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(SNBController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetWallJumpVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        

        player.BodyAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.ArmAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);

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
            wallJumpDirection = -player.FacingDirection; // so the wallJumpdirection should be the opposite of the facing direction
        }
        else
        {
            wallJumpDirection = +player.FacingDirection;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
