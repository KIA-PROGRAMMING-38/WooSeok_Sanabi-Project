using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isGrounded;
    protected bool isGrappled;
    protected bool MouseInput;
    protected bool MouseInputHold;
    protected bool JumpInput;
    protected bool isDamaged;
    


    public PlayerAbilityState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = playerController.CheckIfGrounded();
        isDamaged= playerController.CheckIfDamaged();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MouseInput = playerController.Input.MouseInput;
        if (isAbilityDone)
        {
            if (isGrounded && playerController.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(playerController.IdleState);
            }
            else
            {
                stateMachine.ChangeState(playerController.InAirState);
            }
        }

        if (isDamaged)
        {
            stateMachine.ChangeState(playerController.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
