using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.audioManager.Play("playerRun");
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.audioManager.Stop("playerRun");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerController.CheckIfShouldFlip(InputX);
        playerController.SetVelocityX(playerData.runVelocity * InputX);
        if (InputX == 0 && !isExitingState)
        {
            stateMachine.ChangeState(playerController.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
