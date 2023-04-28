using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHitState : PlayerBossState
{
    public PlayerGetHitState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        playerController.OnGetHit -= TemporaryZoomInPlayer;
        playerController.OnGetHit += TemporaryZoomInPlayer;
        playerController.InvokeOnGetHit();
        //playerController.transform.position = GameManager.Instance.playerGrabPos.position;
    }

    public override void Exit()
    {
        base.Exit();
        playerController.OnGetHit -= TemporaryZoomInPlayer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //HoldPosition();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void TemporaryZoomInPlayer()
    {
        GameManager.Instance.cameraFollow.StartTemporaryZoomInPlayer();
    }
}
