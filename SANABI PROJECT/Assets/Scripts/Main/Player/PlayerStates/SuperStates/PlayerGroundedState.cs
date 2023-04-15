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

        isGrounded = player.CheckIfGrounded();
        isTouchingWall= player.CheckIfTouchingWall();
        isDamaged= player.CheckIfDamaged();
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

        InputX = player.Input.MovementInput.x;
        JumpInput = player.Input.JumpInput;
        MouseInput = player.Input.MouseInput;
        if (JumpInput)
        {
            player.Input.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (MouseInput)
        {
            player.Input.UseJumpInput();
            stateMachine.ChangeState(player.WireShootState);
        }
        else if (isDamaged)
        {
            stateMachine.ChangeState(player.DamagedState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
