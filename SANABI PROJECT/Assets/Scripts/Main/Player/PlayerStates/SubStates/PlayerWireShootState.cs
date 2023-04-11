using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireShootState : PlayerAbilityState
{
    Vector2 holdPosition;
    Vector2 shootDirection;
    public PlayerWireShootState(SNBController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        holdPosition = player.transform.position;
        shootDirection = player.WireController.distanceVector.normalized;
        player.GrabController.ConvertMouseInput(player.Input.MouseInput);
        player.CheckIfShouldFlipForMouseInput(shootDirection.x);
    }

    public override void Exit()
    {
        base.Exit();
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
            stateMachine.ChangeState(player.WireGrappledState);
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
