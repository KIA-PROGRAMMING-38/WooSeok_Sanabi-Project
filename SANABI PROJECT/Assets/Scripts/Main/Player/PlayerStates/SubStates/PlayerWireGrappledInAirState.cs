using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireGrappledInAirState : PlayerWireState
{
    public PlayerWireGrappledInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        playerController.AddXVelocityWhenGrappled(playerController.Input.MovementInput.x);

        if (DashInput)
        {
            playerController.Input.UseDashInput();
            playerController.PlayerWireDash();
            
        }

        if (isGrounded && playerController.CurrentVelocity.y <= 0f)
        {
            stateMachine.ChangeState(playerController.WireGrappledIdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
