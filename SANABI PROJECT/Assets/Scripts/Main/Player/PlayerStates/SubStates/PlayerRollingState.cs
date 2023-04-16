using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerRollingState : PlayerState
{
    private bool isGrounded;
    private float xInput;
    private bool isDamaged;
    private bool MouseInput;
    public PlayerRollingState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isDamaged = player.CheckIfDamaged();
    }

    public override void Enter()
    {
        base.Enter();
        player.OnDamagedDash -= ChangeToInAirState;
        player.OnDamagedDash += ChangeToInAirState;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.Input.MovementInput.x;
        MouseInput = player.Input.MouseInput;

        if (isGrounded && player.CurrentVelocity.y < 0.01f) // if landed on ground
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isDamaged)
        {
            stateMachine.ChangeState(player.DamagedState);
        }
        else if (MouseInput)
        {
            stateMachine.ChangeState(player.WireShootState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.runVelocity * xInput);


            player.BodyAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.ArmAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);
        }

        FastFall();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChangeToInAirState() // to be called as animation event at the end of animation frames
    {
        stateMachine.ChangeState(player.InAirState);
    }
    private void FastFall()
    {
        if (player.CurrentVelocity.y < 0f)
        {
            player.playerRigidBody.gravityScale = 1.5f;
        }
        else
        {
            player.playerRigidBody.gravityScale = 1.0f;
        }
    }
}

