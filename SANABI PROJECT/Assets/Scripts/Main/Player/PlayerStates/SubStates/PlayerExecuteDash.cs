using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerExecuteDash : PlayerAbilityState
{
    private Vector2 executeDashDirection;
    private Quaternion initialBodyRotation;
    private Quaternion initialArmRotation;
    private float dashRotation;
    private float slowTime;
    private float slowIntensity;
    public PlayerExecuteDash(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        initialBodyRotation = Quaternion.identity;
        initialArmRotation = Quaternion.identity;
        executeDashDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerController.ExecuteDashIconController.transform.position).normalized;
        slowTime = playerData.executeDashSlowTime;
        slowIntensity = playerData.executeDashTimeScale;
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
        playerController.timeSlower.PleaseSlowDown(slowIntensity, slowTime);
        playerController.StartShowAfterImage();
        playerController.SetExecuteDashEffectOn();
        GameManager.Instance.grabController.hasGrabbedTurret = false;
        GameManager.Instance.audioManager.Play("playerExecuteDash");
    }

    public override void Exit()
    {
        base.Exit();
        playerController.transform.rotation = initialBodyRotation;
        playerController.ArmController.transform.rotation = initialArmRotation;
        playerController.OnExecuteDash -= ChangeToRollingState;
        playerController.MakePlayerVulnerable();
        //playerController.StopShowAfterImage();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (MouseInput)
        {
            stateMachine.ChangeState(playerController.WireShootState);
        }
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
                playerController.ArmController.transform.rotation = Quaternion.Euler(0f, 0f, dashRotation);
            }
            else // if down
            {
                playerController.transform.rotation = Quaternion.Euler(0f, 0f, dashRotation - 360f);
                playerController.ArmController.transform.rotation = Quaternion.Euler(0f, 0f, dashRotation - 360f);
            }
        }
        else // if left
        {
            if (0f <= executeDashDirection.y) // if up
            {
                playerController.transform.rotation = Quaternion.Euler(0f, 0f, -(180f - dashRotation));
                playerController.ArmController.transform.rotation = Quaternion.Euler(0f, 0f, -(180f - dashRotation));
            }
            else // if down
            {
                playerController.transform.rotation = Quaternion.Euler(0f, 0f, dashRotation - 180f);
                playerController.ArmController.transform.rotation = Quaternion.Euler(0f, 0f, dashRotation - 180f);
            }
        }
    }

    private void ChangeToRollingState() // to be called as animation event at the end of animation frames
    {
        stateMachine.ChangeState(playerController.RollingState);
    }
}

