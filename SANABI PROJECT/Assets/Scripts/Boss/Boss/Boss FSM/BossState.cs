using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{
    protected BossController bossController;
    protected BossStateMachine stateMachine;
    protected BossData bossData;

    private string animBoolName;

    public BossState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName)
    {
        this.bossController = bossController;
        this.stateMachine = bossStateMachine;
        this.bossData = bossData;
        this.animBoolName= animBoolName;
    }
    public virtual void DoChecks()
    {

    }
    public virtual void Enter()
    {
        DoChecks();
        bossController.BodyAnimator.SetBool(animBoolName, true);
        bossController.HeadAnimator.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        bossController.BodyAnimator.SetBool(animBoolName, false);
        bossController.HeadAnimator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
}
