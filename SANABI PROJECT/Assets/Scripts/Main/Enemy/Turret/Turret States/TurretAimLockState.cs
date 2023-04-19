using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TurretAimLockState : TurretState
{
    public TurretAimLockState(TurretController controller, TurretStateMachine statemachine, TurretData turretdata, string animboolname) : base(controller, statemachine, turretdata, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        turretController.StartAimLock();
        turretController.gunController.StartColorChange();
        turretController.WarningInsideAnimator.SetBool("warningIdle", true);
        turretController.WarningOutsideAnimator.SetBool("warningIdle", true);
    }

    public override void Exit()
    {
        base.Exit();
        turretController.StopAimLock();
        turretController.gunController.StopColorChange();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

