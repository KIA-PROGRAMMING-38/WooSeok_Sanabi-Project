using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarRecoveryIdleState : HPBarState
{
    private float elapsedTime;
    private float transitionCooltime;
    public HPBarRecoveryIdleState(HPBarController follow, HPBarStateMachine statemachine, PlayerHealth playerHealth, string animboolname) : base(follow, statemachine, playerHealth, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        transitionCooltime = playerHealth.transitionCooltime;
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
        if (transitionCooltime <= elapsedTime)
        {
            stateMachine.ChangeState(hpBarController.TransitionToIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
