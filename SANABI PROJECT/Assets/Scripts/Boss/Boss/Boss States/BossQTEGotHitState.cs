using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public class BossQTEGotHitState : BossState
{
    public BossQTEGotHitState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.playerController.OnQTEHitFinished -= ChangeToQTEState;
        GameManager.Instance.playerController.OnQTEHitFinished += ChangeToQTEState;
        bossController.CheckIfShouldFlip();
        GameManager.Instance.playerController.transform.position = GameManager.Instance.playerGrabPos.position;
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.playerController.OnQTEHitFinished -= ChangeToQTEState;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    private void ChangeToQTEState()
    {
        stateMachine.ChangeState(bossController.QTEState);
    }
}

