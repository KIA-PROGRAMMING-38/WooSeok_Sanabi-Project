using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireGrappledWalkState : PlayerWireState
{
    public PlayerWireGrappledWalkState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        hasGrabBeenDisabled = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.CheckIfShouldFlip(InputX);
        player.SetVelocityX(playerData.walkVelocity * InputX);

        if (!isExitingState)
        {
            if (InputX == 0)
            {
                stateMachine.ChangeState(player.WireGrappledIdleState);
            }
            else if (!isGrounded)
            {
                stateMachine.ChangeState(player.WireGrappledInAirState);
            }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
