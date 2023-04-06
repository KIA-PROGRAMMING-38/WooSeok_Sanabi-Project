using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireState : PlayerState
{
    public bool MouseInput;
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        MouseInput = player.Input.MouseInput;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
