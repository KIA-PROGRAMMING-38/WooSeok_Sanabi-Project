using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQTEState : BossInteractionState
{
    public BossQTEState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        //GameManager.Instance.bossGunController.lineRenderer.enabled = false;
        //bossController.CheckIfShouldFlip();
        //GameManager.Instance.playerController.transform.position = GameManager.Instance.playerGrabPos.position;
        GameManager.Instance.bossCanvasController.bossTransform = bossController.transform;
        GameManager.Instance.bossCanvasController.TurnOnSlider();
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishAllPhase -= ChangeToEvadeToPhase2State;
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishAllPhase += ChangeToEvadeToPhase2State;
        GameManager.Instance.bossCanvasController.sliderScript.OnFailAnyPhase -= ChangeToEvadeState;
        GameManager.Instance.bossCanvasController.sliderScript.OnFailAnyPhase += ChangeToEvadeState;
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishAllPhase -= ChangeToEvadeToPhase2State;
        GameManager.Instance.bossCanvasController.sliderScript.OnFailAnyPhase -= ChangeToEvadeState;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
    }
    
    private void ChangeToEvadeToPhase2State()
    {
        stateMachine.ChangeState(bossController.EvadeToPhase2State);
    }

    private void ChangeToEvadeState()
    {
        stateMachine.ChangeState(bossController.EvadeState);
    }

}
