using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private float xInput;
    public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks() 
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
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

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.BodyAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.ArmAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
