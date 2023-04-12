using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireShootState : PlayerAbilityState
{
    Vector2 holdPosition;
    Vector2 shootDirection;
    private Quaternion initialArmRotation;
    public PlayerWireShootState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Enter()
    {
        base.Enter();
        holdPosition = player.transform.position;
        
        shootDirection = player.ArmController.distanceVector.normalized;
        player.GrabController.ConvertMouseInput(player.Input.MouseInput);
        player.CheckIfShouldFlipForMouseInput(shootDirection.x);
        initialArmRotation = player.armTransform.rotation;
        player.ArmController.ArmRotateTowardsCursor();

    }

    public override void Exit()
    {
        base.Exit();
        // player.armTransform.rotation = Quaternion.identity;
        player.armTransform.rotation = initialArmRotation;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isGrounded)
        {
            HoldPositionX();
        }

        

        if (player.GrabController.CheckIfGrabReturned())
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (player.GrabController.isGrappled)
        {
            if (isGrounded)
            {
                stateMachine.ChangeState(player.WireGrappledIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.WireGrappledInAirState);
            }
            
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void HoldPositionX()
    {
        player.transform.position.Set(holdPosition.x, player.transform.position.y, player.transform.position.z);
        player.SetVelocityX(0);
    }
}
