using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(SNBController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        // base.LogicUpdate();
        player.SetVelocityY(playerData.wallClimbVelocity);


        if (yInput != 1 && !isExitingState)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        base.LogicUpdate();
    }
}
