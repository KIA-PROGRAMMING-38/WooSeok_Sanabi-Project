using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRobotWaitForRecoveryState : HPRobotState
{
    private float elapsedTime;
    private float recoveryCooltime;
    private float timeOffSet;
    public HPRobotWaitForRecoveryState(HPRobotController follow, HPRobotStateMachine statemachine, PlayerHealth playerHealth, string animboolname) : base(follow, statemachine, playerHealth, animboolname)
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
        //timeOffSet = startTime - damageEnterTime;
    }

    public override void Exit()
    {
        base.Exit();
        elapsedTime = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        elapsedTime += Time.deltaTime;
        if (recoveryCooltime <= elapsedTime)
        {
            //elapsedTime = 0f;
            stateMachine.ChangeState(hpRobotController.RecoveryState);
        }

        else if (isPlayerDamaged && playerHealth.invicibleTime <= elapsedTime)
        {
            //elapsedTime = 0f;
            stateMachine.ChangeState(hpRobotController.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
