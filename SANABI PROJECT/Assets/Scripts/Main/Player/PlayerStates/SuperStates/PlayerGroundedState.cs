using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float InputX;
    private bool JumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    protected bool MouseInput;
    protected bool isDamaged;

    public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = playerController.CheckIfGrounded();
        isTouchingWall= playerController.CheckIfTouchingWall();
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

        InputX = playerController.Input.MovementInput.x;
        JumpInput = playerController.Input.JumpInput;
        MouseInput = playerController.Input.MouseInput;
        if (JumpInput)
        {
            playerController.Input.UseJumpInput();
            stateMachine.ChangeState(playerController.JumpState);
        }
        else if (!isGrounded)
        {
            stateMachine.ChangeState(playerController.InAirState);
        }
        else if (MouseInput)
        {
            playerController.Input.UseJumpInput();
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
