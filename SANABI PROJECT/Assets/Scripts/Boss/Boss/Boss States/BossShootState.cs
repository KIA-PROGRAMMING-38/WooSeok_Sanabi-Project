using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootState : BossState
{
    public BossShootState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        bossController.StartWaitShootTime();
    }

    public override void Exit()
    {
        base.Exit();
        bossController.StopWaitShootTime();
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
