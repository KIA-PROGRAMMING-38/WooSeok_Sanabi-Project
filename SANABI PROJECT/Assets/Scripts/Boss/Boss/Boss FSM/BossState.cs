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
    protected bool ifQTE;

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
        ifQTE = bossController.CheckIfQTE();
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
            
            bossController.CheckIfShouldFlip();
            if (isPhase1)
            {
                if (!ifQTE)
                {
                    stateMachine.ChangeState(bossController.EvadeState);
                }
                else
                {
                    stateMachine.ChangeState(bossController.QTEState);
                }
            }
            else // if phase 2
            {
                if (!bossController.isBossReadyToBeExecuted)
                {
                    stateMachine.ChangeState(bossController.EvadeState);
                }
                else
                {
                    stateMachine.ChangeState(bossController.ExecutedState);
                    
                }
                
            }
            
        }
        
        
    }
}
