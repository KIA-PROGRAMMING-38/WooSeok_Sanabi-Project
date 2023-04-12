using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerWireState : PlayerState
{
    protected bool MouseHoldInput;
    protected bool DashInput;
    protected float InputX;
    public bool MouseInput;
    public bool isGrounded;
    protected bool hasGrabBeenDisabled;

    public PlayerWireState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if (!hasGrabBeenDisabled)
        {
            player.ArmController.ConnectAnchor();
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        player.armTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player.PlayerWireDashStop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MouseHoldInput = player.Input.MouseInputHold;
        DashInput = player.Input.DashInput;
        InputX = player.Input.MovementInput.x;
        MouseInput = player.Input.MouseInput;
        isGrounded = player.CheckIfGrounded();

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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }



}
