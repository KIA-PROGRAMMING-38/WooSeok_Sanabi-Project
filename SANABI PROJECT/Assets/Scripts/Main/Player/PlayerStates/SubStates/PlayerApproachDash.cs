using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;


public class PlayerApproachDash : PlayerAbilityState
{
    private Vector3 grabbedTurretPosition;
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
        playerController.MakePlayerInvicible();
        playerController.OnApproachDashToTurret -= ChangeToExecuteHoldedState;
        playerController.OnApproachDashToTurret += ChangeToExecuteHoldedState;
        playerController.PlayerIsDash(true);
        playerController.SetVelocityAll(approachDashDirection.x * ApproachDashForce, approachDashDirection.y * ApproachDashForce);
        playerController.PlayerApproachDash();
    }

    public override void Exit()
    {
        base.Exit();
        playerController.PlayerIsDash(false);
        playerController.OnApproachDashToTurret -= ChangeToExecuteHoldedState;
        playerController.MakePlayerVulnerable();
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
        grabbedTurretPosition = playerController.GrabController.GetGrabbedTurretPosition();
        approachDashDirection = grabbedTurretPosition - playerController.transform.position;
        approachDashDirection.Normalize();
    }

    private void ChangeToExecuteHoldedState()
    {
        stateMachine.ChangeState(playerController.ExecuteHolded);
    }
}

