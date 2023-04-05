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
        holdPosition = player.transform.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();
        if (0f < yInput) // if pressing W 
        {
            stateMachine.ChangeState(player.WallClimbState);
        }
        else if (yInput < 0f)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
    }

    private void HoldPosition()
    {
        player.transform.position = holdPosition;
        player.SetVelocityX(0);
        player.SetVelocityY(0);
    }
}