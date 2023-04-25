using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHitState : PlayerBossState
{
    public PlayerGetHitState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        HoldPosition();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void HoldPosition()
    {
        playerController.SetVelocityAll(0f, 0f);
    }
}
