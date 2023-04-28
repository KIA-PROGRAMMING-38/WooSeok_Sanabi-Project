using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteractionState : BossState
{
    public BossInteractionState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.playerController.transform.position = GameManager.Instance.playerGrabBossPos.position;
        GameManager.Instance.bossGunController.lineRenderer.enabled = false;
        GameManager.Instance.grabController.hasGrabbedBoss = false;
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
