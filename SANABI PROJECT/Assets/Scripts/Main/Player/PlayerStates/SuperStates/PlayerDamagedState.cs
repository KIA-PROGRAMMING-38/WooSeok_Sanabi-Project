using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDamagedState : PlayerState
{
    protected bool isGrounded;
    protected Vector2 damagedJumpDirection;
    protected float elapsedTime;
    protected float invincibleTime;
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
        isGrounded = player.CheckIfGrounded();
        cameraShakeTime = player.camShake.damagedShakeTime;
        cameraShakeIntensity = player.camShake.damagedShakeIntensity;
        slowTime = playerData.slowTime;
        slowIntensity = playerData.timeScale;
        
    }

    
    public override void Enter()
    {
        base.Enter();
        player.playerHealth.OnDead -= ChangeToDeadState;
        player.playerHealth.OnDead += ChangeToDeadState;

        player.playerHealth.TakeDamage(playerData.PlayerTakeDamage); // got to change magic number
        
        damagedJumpDirection = Vector2.up + Vector2.right; // (1, 1)
        invincibleTime = playerData.invincibleTime;
        DamagedJumpBack(player.FacingDirection);
        player.camShake.TurnOnShake(cameraShakeTime, cameraShakeIntensity);
        player.camFollow.ChangeColor();
        player.timeSlower.PleaseSlowDown(slowIntensity, slowTime);
    }

    public override void Exit()
    {
        base.Exit();
        player.ResetDamageState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        InputX = player.Input.MovementInput.x;
        InputY = player.Input.MovementInput.y;
        JumpInput = player.Input.JumpInput;

        CheckIfTryingToDamagedDash();

        elapsedTime += Time.deltaTime;
        if (invincibleTime <= elapsedTime) 
        {
            elapsedTime = 0f;
            if (isGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }

        if (isTryingToDash)
        {
            isTryingToDash = false; // test code got to delete later
            player.SetDamagedDashVelocity(InputX, InputY, playerData.damagedDashVelocity);
            stateMachine.ChangeState(player.DamagedDashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void DamagedJumpBack(int FacingDirection)
    {
        damagedJumpDirection.x = -FacingDirection * damagedJumpDirection.x;
        player.SetVelocityAll(damagedJumpDirection.x * playerData.damagedJumpVelocity, damagedJumpDirection.y * playerData.damagedJumpVelocity);
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
        stateMachine.ChangeState(player.DeadState);
    }
}
