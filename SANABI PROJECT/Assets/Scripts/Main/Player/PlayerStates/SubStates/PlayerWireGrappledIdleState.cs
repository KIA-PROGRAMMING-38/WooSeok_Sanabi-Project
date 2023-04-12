using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireGrappledIdleState : PlayerWireState
{
    public PlayerWireGrappledIdleState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        if (InputX != 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.WireGrappledWalkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
