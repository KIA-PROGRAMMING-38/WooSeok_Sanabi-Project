using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;


public class PlayerExecuteHolded : PlayerAbilityState
{
    private Vector2 collisionPosition;
    private Vector2 upperPosition = new Vector2(0f, 1f);
    public PlayerExecuteHolded(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        collisionPosition = playerController.GrabController.GetGrabbedTurretPosition();
    }

    public override void Enter()
    {
        base.Enter();
        //playerController.transform.position = collisionPosition;
        playerController.ExecuteDashIconAnimator.SetBool("isGrabbing", true);
        playerController.transform.position = collisionPosition + upperPosition;
        playerController.SetVelocityAll(0f,0f);
        playerController.playerRigidBody.gravityScale = 0f;
        playerController.ExecuteDashIconController.StartFollowingCursor();
        playerController.StartExecuteHolded();
    }

    public override void Exit()
    {
        base.Exit();
        playerController.playerRigidBody.gravityScale = 1f;
        playerController.ExecuteDashIconAnimator.SetBool("isGrabbing", false);
        playerController.ExecuteDashIconController.StopFollowingCursor();
        playerController.ArmController.PlayerGrabOffTurret();
        playerController.StopExecuteHolded();
        playerController.TurretHasBeenReleased();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MouseInputHold = playerController.Input.MouseInputHold;

        if (!MouseInputHold)
        {
            stateMachine.ChangeState(playerController.ExecuteDash);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

