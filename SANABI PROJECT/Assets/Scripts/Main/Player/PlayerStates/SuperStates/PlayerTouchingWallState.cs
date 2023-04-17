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
    protected bool MouseInput;
    protected bool isDamaged;

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

        isGrounded = playerController.CheckIfGrounded();
        isTouchingWall = playerController.CheckIfTouchingWall();
        isDamaged= playerController.CheckIfDamaged();
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
        yInput = playerController.Input.MovementInput.y;
        JumpInput = playerController.Input.JumpInput;
        MouseInput = playerController.Input.MouseInput;

        if (isGrounded)
        {
            stateMachine.ChangeState(playerController.IdleState);
        }
        else if (!isTouchingWall)// || xInput != player.FacingDirection)
        {
            stateMachine.ChangeState(playerController.InAirState);
        }
        else if (isTouchingWall && JumpInput) //  && (isTouchingWall || isTouchingWallBack)
        {
            playerController.Input.UseJumpInput();
            playerController.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(playerController.WallJumpState);
        }
        else if (isTouchingWall && xInput == -playerController.FacingDirection) // to get off the wall if pressing the button opposite to the facing direction
        {
            xInputTime += Time.deltaTime;
            if (playerData.wallGrabOffSeconds <= xInputTime)
            {
                stateMachine.ChangeState(playerController.InAirState);
                xInputTime = 0f;
            }
        }
        else if (MouseInput)
        {
            playerController.Input.UseWireShoot();
            stateMachine.ChangeState(playerController.WireShootState);
        }
        else if (isDamaged)
        {
            stateMachine.ChangeState(playerController.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
