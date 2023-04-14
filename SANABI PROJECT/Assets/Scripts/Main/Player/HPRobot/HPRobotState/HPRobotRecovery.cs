using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRobotRecovery : HPRobotState
{
    
    public HPRobotRecovery(HPRobotController follow, HPRobotStateMachine statemachine, PlayerHealth playerHealth, string animboolname) : base(follow, statemachine, playerHealth, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        playerHealth.OnRecoverHP -= UpdateRecoveredHP;
        playerHealth.OnRecoverHP += UpdateRecoveredHP;
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

    private void UpdateRecoveredHP(int recoveredHP)
    {
        hpRobotController.animator.SetInteger(playerHPName, recoveredHP);
    }
}
