using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossState : PlayerState
{
    protected bool MouseInput;
    protected bool MouseInputHold;
    public PlayerBossState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        playerController.isPlayerBossState = true;
        //playerController.transform.position = GameManager.Instance.playerGrabPos.position;
        playerController.SetVelocityAll(0f, 0f);
    }

    public override void Exit()
    {
        base.Exit();
        playerController.isPlayerBossState = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MouseInput = playerController.Input.MouseInput;
        MouseInputHold = playerController.Input.MouseInputHold; 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
