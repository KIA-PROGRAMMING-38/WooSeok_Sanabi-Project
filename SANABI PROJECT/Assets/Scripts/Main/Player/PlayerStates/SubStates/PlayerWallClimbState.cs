using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerController.stillOnWall = true;
        GameManager.Instance.audioManager.Play("playerWallClimb");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerController.SetVelocityY(playerData.wallClimbVelocity);


        if (yInput != 1 && !isExitingState)
        {
            stateMachine.ChangeState(playerController.WallGrabState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.audioManager.Stop("playerWallClimb");
    }
}
