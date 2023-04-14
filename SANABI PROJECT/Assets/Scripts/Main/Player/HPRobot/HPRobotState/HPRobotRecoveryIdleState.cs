using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRobotRecoveryIdleState : HPRobotState
{
    private float elapsedTime;
    private float transitionCooltime;
    public HPRobotRecoveryIdleState(HPRobotController follow, HPRobotStateMachine statemachine, PlayerHealth playerHealth, string animboolname) : base(follow, statemachine, playerHealth, animboolname)
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
        playerHealth.OnResetHP -= UpdateResetHP;
        playerHealth.OnResetHP += UpdateResetHP;
        playerHealth.ResetToMaxHP();
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
        if (transitionCooltime <= elapsedTime)
        {
            stateMachine.ChangeState(hpRobotController.TransitionToIdleState);
        }
        else if (isPlayerDamaged && playerHealth.invicibleTime <= elapsedTime)
        {
            stateMachine.ChangeState(hpRobotController.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void UpdateResetHP(int maxHP)
    {
        hpRobotController.animator.SetInteger(playerHPName, maxHP);
    }
}
