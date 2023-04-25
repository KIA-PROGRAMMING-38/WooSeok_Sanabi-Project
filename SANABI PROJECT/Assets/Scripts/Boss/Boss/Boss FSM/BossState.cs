using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{
    protected BossController bossController;
    protected BossStateMachine stateMachine;
    protected BossData bossData;

    private string animBoolName;

    protected bool isPhase1;
    protected bool ifGoToPhase2;

    public BossState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName)
    {
        this.bossController = bossController;
        this.stateMachine = bossStateMachine;
        this.bossData = bossData;
        this.animBoolName= animBoolName;
    }
    public virtual void DoChecks()
    {
        isPhase1 = bossController.CheckIfPhase1();
        ifGoToPhase2 = bossController.CheckIfGoToPhase2();
    }
    public virtual void Enter()
    {
        DoChecks();
        GameManager.Instance.playerController.OnApproachDashToBoss -= ChangeTo_Evade_OR_QTEState;
        GameManager.Instance.playerController.OnApproachDashToBoss += ChangeTo_Evade_OR_QTEState;
        bossController.BodyAnimator.SetBool(animBoolName, true);
        bossController.HeadAnimator.SetBool(animBoolName, true);
        
    }

    public virtual void Exit()
    {
        GameManager.Instance.playerController.OnApproachDashToBoss -= ChangeTo_Evade_OR_QTEState;
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

    private void ChangeTo_Evade_OR_QTEState()
    {
        if (GameManager.Instance.grabController.hasGrabbedBoss)
        {
            if (!ifGoToPhase2)
            {
                stateMachine.ChangeState(bossController.EvadeState);
            }
            else
            {
                stateMachine.ChangeState(bossController.QTEState);
            }
        }
        
        
    }
}
