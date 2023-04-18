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
    private bool isTouchingWall;
    public PlayerRollingState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = playerController.CheckIfGrounded();
        isDamaged = playerController.CheckIfDamaged();
        isTouchingWall = playerController.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
        playerController.OnDamagedDash -= ChangeToInAirState;
        playerController.OnDamagedDash += ChangeToInAirState;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = playerController.Input.MovementInput.x;
        MouseInput = playerController.Input.MouseInput;

        if (isGrounded && playerController.CurrentVelocity.y < 0.01f) // if landed on ground
        {
            stateMachine.ChangeState(playerController.LandState);
        }
        else if (isDamaged)
        {
            stateMachine.ChangeState(playerController.DamagedState);
        }
        else if (isTouchingWall && xInput == playerController.FacingDirection) // if xInput is in the direction of the wall
        {
            stateMachine.ChangeState(playerController.WallGrabState);
        }
        else if (MouseInput)
        {
            stateMachine.ChangeState(playerController.WireShootState);
        }
        else
        {
            playerController.CheckIfShouldFlip(xInput);
            playerController.SetVelocityX(playerData.runVelocity * xInput);


            playerController.BodyAnimator.SetFloat("yVelocity", playerController.CurrentVelocity.y);
            playerController.ArmAnimator.SetFloat("yVelocity", playerController.CurrentVelocity.y);
        }

        FastFall();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChangeToInAirState() // to be called as animation event at the end of animation frames
    {
        stateMachine.ChangeState(playerController.InAirState);
    }
    private void FastFall()
    {
        if (playerController.CurrentVelocity.y < 0f)
        {
            playerController.playerRigidBody.gravityScale = 1.5f;
        }
        else
        {
            playerController.playerRigidBody.gravityScale = 1.0f;
        }
    }
}

