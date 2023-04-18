using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDamagedState : PlayerState
{
    protected bool isGrounded;
    protected Vector2 damagedJumpDirection;
    protected float elapsedTime;
    protected float damagedOutTime;
    private float cameraShakeTime;
    private float cameraShakeIntensity;
    private float slowTime;
    private float slowIntensity;

    private float InputX;
    private float InputY;
    private bool JumpInput;
    private bool isTryingToDash;
    public PlayerDamagedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = playerController.CheckIfGrounded();
        cameraShakeTime = playerController.camShake.damagedShakeTime;
        cameraShakeIntensity = playerController.camShake.damagedShakeIntensity;
        slowTime = playerData.slowTime;
        slowIntensity = playerData.timeScale;
        
    }

    
    public override void Enter()
    {
        base.Enter();
        playerController.playerHealth.OnDead -= ChangeToDeadState;
        playerController.playerHealth.OnDead += ChangeToDeadState;

        playerController.playerHealth.TakeDamage(playerData.PlayerTakeDamage); // got to change magic number
        
        damagedJumpDirection = Vector2.up + Vector2.right; // (1, 1)
        damagedOutTime = playerData.damagedOutTime;
        DamagedJumpBack(playerController.FacingDirection);
        playerController.camShake.TurnOnShake(cameraShakeTime, cameraShakeIntensity);
        playerController.camFollow.ChangeColor();
        playerController.timeSlower.PleaseSlowDown(slowIntensity, slowTime);
    }

    public override void Exit()
    {
        base.Exit();
        playerController.ResetDamageState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        InputX = playerController.Input.MovementInput.x;
        InputY = playerController.Input.MovementInput.y;
        JumpInput = playerController.Input.JumpInput;

        CheckIfTryingToDamagedDash();

        elapsedTime += Time.deltaTime;
        if (damagedOutTime <= elapsedTime) 
        {
            elapsedTime = 0f;
            if (isGrounded)
            {
                stateMachine.ChangeState(playerController.IdleState);
            }
            else
            {
                stateMachine.ChangeState(playerController.InAirState);
            }
        }

        if (isTryingToDash)
        {
            isTryingToDash = false; // test code got to delete later
            playerController.SetDamagedDashVelocity(InputX, InputY, playerData.damagedDashVelocity);
            stateMachine.ChangeState(playerController.DamagedDashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void DamagedJumpBack(int FacingDirection)
    {
        damagedJumpDirection.x = -FacingDirection * damagedJumpDirection.x;
        playerController.SetVelocityAll(damagedJumpDirection.x * playerData.damagedJumpVelocity, damagedJumpDirection.y * playerData.damagedJumpVelocity);
    }

    private void CheckIfTryingToDamagedDash()
    {
        if ((InputX != 0 || InputY != 0) && JumpInput)
        {
            isTryingToDash = true;
        }
    }

    private void ChangeToDeadState()
    {
        stateMachine.ChangeState(playerController.DeadState);
    }
}
