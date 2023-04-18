using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerExecuteDash : PlayerAbilityState
{
    private Vector2 executeDashDirection;
    private Quaternion initialRotation;
    private float dashRotation;
    public PlayerExecuteDash(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        initialRotation = Quaternion.identity;
        executeDashDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerController.ExecuteDashIconController.transform.position).normalized;
    }

    public override void Enter()
    {
        base.Enter();
        playerController.MakePlayerInvicible();
        playerController.SetVelocityAll(executeDashDirection.x * playerData.executeDashVelocity, executeDashDirection.y * playerData.executeDashVelocity);
        playerController.CheckIfShouldFlip(executeDashDirection.x);
        RotatePlayer();
        playerController.OnExecuteDash -= ChangeToRollingState;
        playerController.OnExecuteDash += ChangeToRollingState;
    }

    public override void Exit()
    {
        base.Exit();
        playerController.transform.rotation = initialRotation;
        playerController.OnExecuteDash -= ChangeToRollingState;
        playerController.MakePlayerVulnerable(); 
        // execute dash 실험하기
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void RotatePlayer()
    {
        dashRotation = Mathf.Atan2(executeDashDirection.y, executeDashDirection.x) * Mathf.Rad2Deg;

        if (0f <= executeDashDirection.x) // if point right
        {
            if (0f <= executeDashDirection.y) // if up
            {
                playerController.transform.rotation = Quaternion.Euler(0f, 0f, dashRotation);
            }
            else // if down
            {
                playerController.transform.rotation = Quaternion.Euler(0f, 0f, dashRotation - 360f);
            }
        }
        else // if left
        {
            if (0f <= executeDashDirection.y) // if up
            {
                playerController.transform.rotation = Quaternion.Euler(0f, 0f, -(180f - dashRotation));
            }
            else // if down
            {
                playerController.transform.rotation = Quaternion.Euler(0f, 0f, dashRotation - 180f);
            }
        }
    }

    private void ChangeToRollingState() // to be called as animation event at the end of animation frames
    {
        stateMachine.ChangeState(playerController.RollingState);
    }
}

