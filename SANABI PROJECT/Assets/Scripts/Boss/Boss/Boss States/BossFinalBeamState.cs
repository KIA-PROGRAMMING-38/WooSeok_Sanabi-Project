using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class BossFinalBeamState : BossState
{
    public BossFinalBeamState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.audioManager.Play("bossFinalBeam");
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
