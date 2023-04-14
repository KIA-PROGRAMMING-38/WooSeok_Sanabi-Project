using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarWaitForRecoveryState : HPBarState
{
    private float elapsedTime;
    private float recoveryCooltime;
    public HPBarWaitForRecoveryState(HPBarController follow, HPBarStateMachine statemachine, PlayerHealth playerHealth, string animboolname) : base(follow, statemachine, playerHealth, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        recoveryCooltime = playerHealth.recoveryCooltime;
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
        elapsedTime += Time.deltaTime;
        if (recoveryCooltime <= elapsedTime)
        {
            elapsedTime = 0f;
            stateMachine.ChangeState(hpBarController.RecoveryState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
