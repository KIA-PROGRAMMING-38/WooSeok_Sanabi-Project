using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAimLockState : BossState
{
    public BossAimLockState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        bossController.bossGunController.OnFinishedAimLock -= ChangeToReadyToShootState;
        bossController.bossGunController.OnFinishedAimLock += ChangeToReadyToShootState;

        bossController.bossGunController.StartAimLock();
        bossController.bossGunController.StartAimLineColorChange();
    }

    public override void Exit()
    {
        base.Exit();
        bossController.bossGunController.StopAimLock();
        bossController.bossGunController.StopAimLineColorChange();
    }

    private void ChangeToReadyToShootState()
    {
        stateMachine.ChangeState(bossController.ReadyToShootState);
    }
}
