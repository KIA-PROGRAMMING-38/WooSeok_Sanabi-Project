using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{

    public PlayerWallSlideState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityY(-playerData.wallSlideVelocity); // put - in front of the wallSlideVelocity so it can drop, not up
        if (yInput != -1)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    
    }
}
