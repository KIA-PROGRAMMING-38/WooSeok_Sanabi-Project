using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQTEState : PlayerBossState
{
    
    public PlayerQTEState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        playerController.OnQTE -= EternalZoomInPlayer;
        playerController.OnQTE += EternalZoomInPlayer;
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishClickPhase -= ChangeToQTEHitState;
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishClickPhase += ChangeToQTEHitState;
        
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishAllPhase -= ChangeToEvadeToPhase2State;
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishAllPhase += ChangeToEvadeToPhase2State;

        GameManager.Instance.bossCanvasController.sliderScript.OnFailAnyPhase -= ChangeToGetHitState;
        GameManager.Instance.bossCanvasController.sliderScript.OnFailAnyPhase += ChangeToGetHitState;
        playerController.InvokeOnQTE();
    }

    public override void Exit()
    {
        base.Exit();
        playerController.OnQTE -= EternalZoomInPlayer;
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishClickPhase -= ChangeToQTEHitState;
        GameManager.Instance.bossCanvasController.sliderScript.OnFinishAllPhase -= ChangeToEvadeToPhase2State;
        GameManager.Instance.bossCanvasController.sliderScript.OnFailAnyPhase -= ChangeToGetHitState;
        //EternalZoomOutPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (MouseInput)
        {
            GameManager.Instance.bossCanvasController.IncreaseSliderGuageWhenClick();
            GameManager.Instance.audioManager.Play("QTEClick");
        }
        else if (MouseInputHold)
        {
            GameManager.Instance.bossCanvasController.IncreaseSliderGuageWhenHold();
        }
    }

    private void EternalZoomInPlayer()
    {
        GameManager.Instance.cameraFollow.StartEternalZoomInPlayer();
    }

    private void EternalZoomOutPlayer()
    {
        GameManager.Instance.cameraFollow.StopEternalZoomOutPlayer();
    }

    private void ChangeToQTEHitState()
    {
        stateMachine.ChangeState(playerController.QTEHitState);
    }

    private void ChangeToEvadeToPhase2State()
    {
        stateMachine.ChangeState(playerController.EvadeToPhase2State);
    }

    private void ChangeToGetHitState()
    {
        stateMachine.ChangeState(playerController.GetHitState);
    }
}
