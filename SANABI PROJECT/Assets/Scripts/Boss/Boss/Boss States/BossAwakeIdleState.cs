using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BossAwakeIdleState : BossState
{
    public BossAwakeIdleState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        bossController.isBossReadyToBeFinished = true;
        GameManager.Instance.playerController.OnFinishBoss -= ChangeToDeadState;
        GameManager.Instance.playerController.OnFinishBoss += ChangeToDeadState;
        //bossController.isBossReadyToDie = true;
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.playerController.OnFinishBoss -= ChangeToDeadState;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        bossController.isBossInFinishRange = bossController.finishChecker.IsPlayerInFinishRange();
    }

    private void ChangeToDeadState()
    {
        stateMachine.ChangeState(bossController.DeadState);
    }
}

