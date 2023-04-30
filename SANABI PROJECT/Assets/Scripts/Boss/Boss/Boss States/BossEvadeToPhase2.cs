using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class BossEvadeToPhase2 : BossInteractionState
{
    public BossEvadeToPhase2(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        bossController.isPhase1 = false;
        bossController.hitCount = 1;
        //bossController.hitCount = 3; // test
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

