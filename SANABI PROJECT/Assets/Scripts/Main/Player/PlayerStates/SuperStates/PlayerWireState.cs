using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerWireState : PlayerState
{
    protected bool MouseHoldInput;
    protected bool DashInput;
    protected float InputX;
    protected bool MouseInput;
    protected bool isGrounded;
    protected bool hasGrabBeenDisabled;
    protected bool isDamaged;
    protected Quaternion initialArmRotation;

    public PlayerWireState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        initialArmRotation = playerController.armTransform.rotation;
        if (!hasGrabBeenDisabled)
        {
            playerController.ArmController.ConnectAnchor();
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        playerController.armTransform.rotation = initialArmRotation;
        playerController.PlayerWireDashStop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MouseHoldInput = playerController.Input.MouseInputHold;
        DashInput = playerController.Input.DashInput;
        InputX = playerController.Input.MovementInput.x;
        MouseInput = playerController.Input.MouseInput;
        //isGrounded = player.CheckIfGrounded();

        playerController.ArmController.ArmRotateTowardsAnchor();

        if (MouseHoldInput && isGrounded)
        {
            playerController.ArmController.EnableMaxDistanceOnly();
        }
        else
        {
            playerController.ArmController.DisableMaxDistanceOnly();
        }

        if (!MouseHoldInput)
        {
            hasGrabBeenDisabled = false;
            playerController.ArmController.DisconnectAnchor();
            if (isGrounded)
            {
                stateMachine.ChangeState(playerController.IdleState);
            }
            else if (!isGrounded)
            {
                //stateMachine.ChangeState(player.InAirState);
                stateMachine.ChangeState(playerController.RollingState);
            }
        }
        else if (isDamaged)
        {
            hasGrabBeenDisabled = false;
            playerController.ArmController.DisconnectAnchor();
            stateMachine.ChangeState(playerController.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }



}
