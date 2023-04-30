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
    private bool ifPhase1;
    private bool ifQTE;
    public PlayerApproachDash(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        GetDashDirection();
        ApproachDashForce = playerData.approachDashVelocity;
        if (GameManager.Instance.bossController != null)
        {
            ifPhase1 = GameManager.Instance.bossController.CheckIfPhase1();
            ifQTE = GameManager.Instance.bossController.CheckIfQTE();
        }
        
    }

    public override void Enter()
    {
        base.Enter();
        playerController.IgnorePlatformCollision(true);
        playerController.MakePlayerInvicible();
        playerController.OnApproachDashToTurret -= ChangeToExecuteHoldedState;
        playerController.OnApproachDashToTurret += ChangeToExecuteHoldedState;
        playerController.OnApproachDashToBoss -= ChangeTo_GetHit_Or_QTEState;
        playerController.OnApproachDashToBoss += ChangeTo_GetHit_Or_QTEState;
        GameManager.Instance.audioManager.Play("playerApproachDash");
        playerController.StartShowAfterImage();
    }

    public override void Exit()
    {
        base.Exit();
        playerController.IgnorePlatformCollision(false);
        playerController.PlayerIsDash(false);
        playerController.OnApproachDashToTurret -= ChangeToExecuteHoldedState;
        playerController.OnApproachDashToBoss -= ChangeTo_GetHit_Or_QTEState;
        playerController.MakePlayerVulnerable();
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
        if (ifPhase1)
        {
            if (!ifQTE) // qte 조건 발동 전이라면
            {
                stateMachine.ChangeState(playerController.GetHitState);
            }
            else // qte 조건 발동 on 이라면
            {
                stateMachine.ChangeState(playerController.QTEState);
            }
        }
        else
        {
            if (!GameManager.Instance.bossController.isBossReadyToBeExecuted)
            {
                stateMachine.ChangeState(playerController.GetHitState);
            }
            else
            {
                stateMachine.ChangeState(playerController.ExecuteBossState);
            }
            

        }


    }
}

