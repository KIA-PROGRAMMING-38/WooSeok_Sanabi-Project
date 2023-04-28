using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAimingState : BossState
{
    public BossAimingState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        bossController.bossGunController.OnFinishedAiming -= ChangeToAimLockState;
        bossController.bossGunController.OnFinishedAiming += ChangeToAimLockState;

        bossController.bossGunController.StartLookingAtTarget();
        bossController.bossGunController.StartAimingAtTarget();
    }

    public override void Exit()
    {
        base.Exit();
        bossController.bossGunController.OnFinishedAiming -= ChangeToAimLockState;
        bossController.bossGunController.StopLookingAtTarget();
        bossController.bossGunController.StopAimingAtTarget();
    }

    private void ChangeToAimLockState()
    {
        stateMachine.ChangeState(bossController.AimLockState);
    }
}
