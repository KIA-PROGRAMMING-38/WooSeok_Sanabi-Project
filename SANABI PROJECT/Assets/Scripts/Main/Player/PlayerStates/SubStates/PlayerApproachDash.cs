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
    private bool ifGoToPhase2;
    public PlayerApproachDash(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        GetDashDirection();
        ApproachDashForce = playerData.approachDashVelocity;
        ifGoToPhase2 = GameManager.Instance.bossController.CheckIfGoToPhase2();
    }

    public override void Enter()
    {
        base.Enter();
        playerController.IgnorePlatformCollision(true);
        playerController.MakePlayerInvicible();
        //playerController.playerRigidBody.bodyType = RigidbodyType2D.Kinematic;
        playerController.OnApproachDashToTurret -= ChangeToExecuteHoldedState;
        playerController.OnApproachDashToTurret += ChangeToExecuteHoldedState;
        playerController.OnApproachDashToBoss -= ChangeTo_GetHit_Or_QTEState;
        playerController.OnApproachDashToBoss += ChangeTo_GetHit_Or_QTEState;
        //playerController.SetVelocityAll(approachDashDirection.x * ApproachDashForce, approachDashDirection.y * ApproachDashForce);
        
        playerController.StartShowAfterImage();
    }

    public override void Exit()
    {
        base.Exit();
        //playerController.playerRigidBody.bodyType = RigidbodyType2D.Dynamic; 
        playerController.IgnorePlatformCollision(false);
        playerController.PlayerIsDash(false);
        playerController.OnApproachDashToTurret -= ChangeToExecuteHoldedState;
        playerController.OnApproachDashToBoss -= ChangeTo_GetHit_Or_QTEState;
        playerController.MakePlayerVulnerable();
        //playerController.StopShowAfterImage();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerController.TransformMove(approachDashDirection, ApproachDashForce);
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

    private void ChangeTo_GetHit_Or_QTEState()
    {
        if (!ifGoToPhase2) // qte 조건 발동 전이라면
        {
            stateMachine.ChangeState(playerController.GetHitState);
        }
        else // qte 조건 발동 on 이라면
        {
            stateMachine.ChangeState(playerController.QTEState);
        }
        
    }
}

