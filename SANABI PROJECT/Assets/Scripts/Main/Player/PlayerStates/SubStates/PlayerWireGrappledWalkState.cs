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
        playerController.CheckIfShouldFlip(InputX);
        playerController.SetVelocityX(playerData.walkVelocity * InputX);

        if (!isExitingState)
        {
            if (InputX == 0)
            {
                stateMachine.ChangeState(playerController.WireGrappledIdleState);
            }
            else if (!isGrounded)
            {
                stateMachine.ChangeState(playerController.WireGrappledInAirState);
            }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
