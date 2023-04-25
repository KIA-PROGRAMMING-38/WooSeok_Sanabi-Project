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
        playerController.InvokeOnQTE();
    }

    public override void Exit()
    {
        base.Exit();
        playerController.OnQTE -= EternalZoomInPlayer;
        EternalZoomOutPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (MouseInput)
        {
            GameManager.Instance.bossCanvasController.IncreaseSliderGuageWhenClick();
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
}
