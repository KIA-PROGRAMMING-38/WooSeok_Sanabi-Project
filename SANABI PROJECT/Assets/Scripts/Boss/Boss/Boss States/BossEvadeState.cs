using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvadeState : BossInteractionState
{
    public BossEvadeState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        ++bossController.hitCount;
        
        //bossController.CheckIfShouldFlip();
        //GameManager.Instance.playerController.transform.position = GameManager.Instance.playerGrabPos.position;
        //GameManager.Instance.bossGunController.lineRenderer.enabled = false;
        //GameManager.Instance.grabController.hasGrabbedBoss = false;
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
