using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;


public class PlayerApproachDash : PlayerAbilityState
{
    private Vector3 grabbedPosition;
    private Vector3 approachDashDirection;
    private float ApproachDashForce;
    public PlayerApproachDash(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        GetDashDirection();
        ApproachDashForce = playerData.approachDashVelocity;
    }

    public override void Enter()
    {
        base.Enter();
        playerController.IgnorePlatformCollision(true);
        playerController.MakePlayerInvicible();
        //playerController.playerRigidBody.bodyType = RigidbodyType2D.Kinematic;
        playerController.OnApproachDashToTurret -= ChangeToExecuteHoldedState;
        playerController.OnApproachDashToTurret += ChangeToExecuteHoldedState;
        playerController.OnApproachDashToBoss -= ChangeToGetHitState;
        playerController.OnApproachDashToBoss += ChangeToGetHitState;
        //playerController.PlayerIsDash(true);
        playerController.SetVelocityAll(approachDashDirection.x * ApproachDashForce, approachDashDirection.y * ApproachDashForce);
        //playerController.PlayerApproachDash();
        playerController.StartShowAfterImage();
    }

    public override void Exit()
    {
        base.Exit();
        //playerController.playerRigidBody.bodyType = RigidbodyType2D.Dynamic; 
        playerController.IgnorePlatformCollision(false);
        playerController.PlayerIsDash(false);
        playerController.OnApproachDashToTurret -= ChangeToExecuteHoldedState;
        playerController.MakePlayerVulnerable();
        //playerController.StopShowAfterImage();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void GetDashDirection()
    {
        grabbedPosition = playerController.GrabController.GetGrabbedPosition();
        approachDashDirection = grabbedPosition - playerController.transform.position;
        approachDashDirection.Normalize();
    }

    private void ChangeToExecuteHoldedState()
    {
        stateMachine.ChangeState(playerController.ExecuteHolded);
    }

    private void ChangeToGetHitState()
    {
        stateMachine.ChangeState(playerController.GetHitState);
    }
}

