using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerFinishBossState : PlayerBossState
{
    public PlayerFinishBossState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        playerController.camShake.TurnOnShake(playerController.camShake.finishBossShakeTime, playerController.camShake.finishBossShakeIntensity);
        playerController.timeSlower.PleaseSlowDown(playerData.finishBossTimeScale, playerData.finishBossSlowTime);
        //playerController.InvokeOnFinishBoss();
        playerController.Flip();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

