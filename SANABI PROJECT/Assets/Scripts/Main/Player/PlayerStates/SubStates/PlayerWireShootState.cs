using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireShootState : PlayerAbilityState
{
    Vector2 holdPosition;
    Vector2 shootDirection;
    private Quaternion initialArmRotation;
    private float cameraShakeTime;
    private float cameraShakeIntensity;

    public PlayerWireShootState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        cameraShakeTime = playerController.camShake.shootShakeTime;
        cameraShakeIntensity = playerController.camShake.shootShakeIntensity;
    }

    public override void Enter()
    {
        base.Enter();
        holdPosition = playerController.transform.position;
        //isAbilityDone = true;
        shootDirection = playerController.ArmController.distanceVector.normalized;
        playerController.GrabController.ConvertMouseInput(playerController.Input.MouseInput);
        playerController.CheckIfShouldFlipForMouseInput(shootDirection.x);
        initialArmRotation = playerController.armTransform.rotation;
        playerController.ArmController.ArmRotateTowardsCursor();
        playerController.camShake.TurnOnShake(cameraShakeTime, cameraShakeIntensity);
        playerController.SetWireShootEffectOn();
    }

    public override void Exit()
    {
        base.Exit();
        // player.armTransform.rotation = Quaternion.identity;
        playerController.armTransform.rotation = initialArmRotation;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isGrounded)
        {
            HoldPositionX();
        }

        

        if (playerController.GrabController.CheckIfGrabReturned())
        {
            stateMachine.ChangeState(playerController.IdleState);
        }
        else if (playerController.GrabController.isGrappled)
        {
            if (isGrounded)
            {
                stateMachine.ChangeState(playerController.WireGrappledIdleState);
            }
            else
            {
                stateMachine.ChangeState(playerController.WireGrappledInAirState);
            }
            
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void HoldPositionX()
    {
        playerController.transform.position.Set(holdPosition.x, playerController.transform.position.y, playerController.transform.position.z);
        playerController.SetVelocityX(0);
    }
}
