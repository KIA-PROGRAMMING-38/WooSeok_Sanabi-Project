using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerExecuteBossState : PlayerBossState
{
    public PlayerExecuteBossState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        CheckIfShouldFlip();
        GameManager.Instance.cameraFollow.StartEternalZoomInPlayer();

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

    private void CheckIfShouldFlip()
    {
        if (playerController.FacingDirection * GameManager.Instance.bossController.FacingDirection < 0f)
        {
            playerController.Flip();
        }
    }
}

