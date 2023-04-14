using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private Vector2 holdPosition;
    public PlayerDeadState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        holdPosition = player.transform.position;
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
        Debug.Log("�׾");
        HoldPosition();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void HoldPosition()
    {
        player.transform.position = holdPosition;
        player.SetVelocityX(0);
        player.SetVelocityY(0);
    }
}
