using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerPausedState : PlayerState
{
    public PlayerPausedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        //playerController.SetVelocityAll(0f,0f);
        playerController.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerController.BodyAnimator.SetFloat("yVelocity", playerController.CurrentVelocity.y);
        playerController.ArmAnimator.SetFloat("yVelocity", playerController.CurrentVelocity.y);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

