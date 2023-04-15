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

        player.AddXVelocityWhenGrappled(player.Input.MovementInput.x);

        if (DashInput)
        {
            player.Input.UseDashInput();
            player.PlayerWireDash();
        }

        if (isGrounded && player.CurrentVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.WireGrappledIdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
