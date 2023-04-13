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
        isGrounded = player.CheckIfGrounded();
        isDamaged= player.CheckIfDamaged();
    }

    public override void Enter()
    {
        base.Enter();
        initialArmRotation = player.armTransform.rotation;
        if (!hasGrabBeenDisabled)
        {
            player.ArmController.ConnectAnchor();
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        player.armTransform.rotation = initialArmRotation;
        player.PlayerWireDashStop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MouseHoldInput = player.Input.MouseInputHold;
        DashInput = player.Input.DashInput;
        InputX = player.Input.MovementInput.x;
        MouseInput = player.Input.MouseInput;
        //isGrounded = player.CheckIfGrounded();

        player.ArmController.ArmRotateTowardsAnchor();

        if (MouseHoldInput && isGrounded)
        {
            player.ArmController.EnableMaxDistanceOnly();
        }
        else
        {
            player.ArmController.DisableMaxDistanceOnly();
        }

        if (!MouseHoldInput)
        {
            hasGrabBeenDisabled = false;
            player.ArmController.DisconnectAnchor();
            if (isGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (!isGrounded)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
        else if (isDamaged)
        {
            hasGrabBeenDisabled = false;
            player.ArmController.DisconnectAnchor();
            stateMachine.ChangeState(player.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }



}
