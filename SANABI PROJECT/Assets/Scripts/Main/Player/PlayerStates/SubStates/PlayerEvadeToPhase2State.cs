using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerEvadeToPhase2State : PlayerBossState
{
    public PlayerEvadeToPhase2State(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        
        playerController.camShake.TurnOnShake(playerController.camShake.QTEHitShakeTime, playerController.camShake.QTEHitShakeIntensity + 0.2f);
    }

    public override void Exit()
    {
        base.Exit();
        EternalZoomOutPlayer();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void EternalZoomOutPlayer()
    {
        GameManager.Instance.cameraFollow.StopEternalZoomOutPlayer();
    }
}

