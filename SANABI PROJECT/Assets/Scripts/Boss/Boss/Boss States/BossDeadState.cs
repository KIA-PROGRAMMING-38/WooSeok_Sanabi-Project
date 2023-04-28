using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : BossState
{
    private Vector2 backDirection = Vector2.zero;
    public BossDeadState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        bossController.isBossDead = true;
        backDirection.Set((bossController.transform.position - GameManager.Instance.playerController.transform.position).normalized.x, 0f);
        bossController.SetBossVelocity(backDirection * 20f);
        
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
