using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRobotIdleState : HPRobotState
{
    
    public HPRobotIdleState(HPRobotController follow, HPRobotStateMachine statemachine, PlayerHealth playerHealth, string animboolname) : base(follow, statemachine, playerHealth, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        //hpBarController.animator.SetInteger(playerHPName, playerMaxHP);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerDamaged && !isExitingState)
        {
            stateMachine.ChangeState(hpRobotController.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    

}
