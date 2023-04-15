using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedDashState : PlayerAbilityState
{
    Quaternion initialRotation;
    public PlayerDamagedDashState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        initialRotation = Quaternion.identity;
        player.OnDamagedDash -= ChangeToInAirState;
        player.OnDamagedDash += ChangeToInAirState;
        player.SetVelocityAll(player.GetDamagedDashVelocity().x, player.GetDamagedDashVelocity().y);
        player.CheckIfShouldFlip(player.GetDamagedDashVelocity().x);
        player.CheckIfShouldRotate(player.GetDamagedDashVelocity().x, player.GetDamagedDashVelocity().y);
    }

    public override void Exit()
    {
        base.Exit();
        player.transform.rotation = initialRotation;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChangeToInAirState() // to be called as animation event at the end of animation frames
    {
        stateMachine.ChangeState(player.InAirState);
    }
}
