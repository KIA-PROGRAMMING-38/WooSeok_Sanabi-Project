using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerQTEHitState : PlayerBossState
{
    public PlayerQTEHitState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        playerController.OnQTEHitFinished -= ChangeToQTEState;
        playerController.OnQTEHitFinished += ChangeToQTEState;
    }

    public override void Exit()
    {
        base.Exit();
        playerController.OnQTEHitFinished -= ChangeToQTEState;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChangeToQTEState()
    {
        stateMachine.ChangeState(playerController.QTEState);
    }
}

