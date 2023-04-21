using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{

    public PlayerWallSlideState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerController.StartShowWallSlideDust();
        
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerController.SetVelocityY(-playerData.wallSlideVelocity); // put - in front of the wallSlideVelocity so it can drop, not up
        if (yInput != -1 && !isExitingState)
        {
            stateMachine.ChangeState(playerController.WallGrabState);
        }

        //base.LogicUpdate(); // now it's working well after move downwards
    }

    public override void Exit()
    {
        base.Exit();
        playerController.StopShowWallSlideDust();
    }
}
