using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;
    public PlayerWallGrabState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        holdPosition = playerController.transform.position;
        if (!playerController.stillOnWall)
        {
            GameManager.Instance.audioManager.Play("playerWallGrab");
        }
        
    }

    public override void LogicUpdate()
    {
        HoldPosition();
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (0f < yInput) // if pressing W 
            {
                stateMachine.ChangeState(playerController.WallClimbState);
            }
            else if (yInput < 0f)
            {
                stateMachine.ChangeState(playerController.WallSlideState);
            }
        }
        //base.LogicUpdate();
    }

    private void HoldPosition()
    {
        playerController.transform.position = holdPosition;
        playerController.SetVelocityX(0);
        playerController.SetVelocityY(0);
    }
}
