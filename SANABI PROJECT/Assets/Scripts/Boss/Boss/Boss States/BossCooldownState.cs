using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCooldownState : BossState
{
    public BossCooldownState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        bossController.StartWaitCooldownTime();
        GameManager.Instance.audioManager.Play("bossCooldown");
    }

    public override void Exit()
    {
        base.Exit();
        bossController.StopWaitCooldownTime();
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
