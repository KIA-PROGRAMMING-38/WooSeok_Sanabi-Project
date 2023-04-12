using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected float xInput;
    protected float yInput;
    protected bool JumpInput;
    protected float xInputTime; // how long input X is pressed for
    public bool MouseInput;
    public PlayerTouchingWallState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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
        yInput = player.Input.MovementInput.y;
        JumpInput = player.Input.JumpInput;
        MouseInput = player.Input.MouseInput;

        if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall)// || xInput != player.FacingDirection)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && JumpInput) //  && (isTouchingWall || isTouchingWallBack)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isTouchingWall && xInput == -player.FacingDirection) // to get off the wall if pressing the button opposite to the facing direction
        {
            xInputTime += Time.deltaTime;
            if (playerData.wallGrabOffSeconds <= xInputTime)
            {
                stateMachine.ChangeState(player.InAirState);
                xInputTime = 0f;
            }
        }
        else if (MouseInput)
        {
            stateMachine.ChangeState(player.WireShootState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
