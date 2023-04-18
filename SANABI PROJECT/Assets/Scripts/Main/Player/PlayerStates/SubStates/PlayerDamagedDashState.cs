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
        playerController.OnDamagedDash -= ChangeToInAirState;
        playerController.OnDamagedDash += ChangeToInAirState;
        playerController.SetVelocityAll(playerController.GetDamagedDashVelocity().x, playerController.GetDamagedDashVelocity().y);
        playerController.CheckIfShouldFlip(playerController.GetDamagedDashVelocity().x);
        playerController.CheckIfShouldRotate(playerController.GetDamagedDashVelocity().x, playerController.GetDamagedDashVelocity().y);
        playerController.StartShowAfterImage();
    }

    public override void Exit()
    {
        base.Exit();
        playerController.transform.rotation = initialRotation;
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
        stateMachine.ChangeState(playerController.InAirState);
    }
}
