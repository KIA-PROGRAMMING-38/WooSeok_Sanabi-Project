using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWireState : PlayerState
{
    protected float InputX;
    public bool MouseInput;
    public bool isGrounded;
    public PlayerWireState(SNBController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        InputX = player.Input.MovementInput.x;
        MouseInput = player.Input.MouseInput;
        isGrounded = player.CheckIfGrounded();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
