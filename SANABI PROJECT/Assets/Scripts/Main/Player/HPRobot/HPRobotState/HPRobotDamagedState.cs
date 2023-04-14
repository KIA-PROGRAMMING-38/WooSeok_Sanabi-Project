using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRobotDamagedState : HPRobotState
{
    public HPRobotDamagedState(HPRobotController follow, HPRobotStateMachine statemachine, PlayerHealth playerHealth, string animboolname) : base(follow, statemachine, playerHealth, animboolname)
    {
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        //damageEnterTime = startTime; why does this not work....????????
        playerHealth.OnDead -= HPRobotDie;
        playerHealth.OnDead += HPRobotDie;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void HPRobotDie()
    {
        hpRobotController.gameObject.SetActive(false);
    }
}
