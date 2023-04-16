using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TurretAimingState : TurretState
{
    public TurretAimingState(TurretController controller, TurretStateMachine statemachine, TurretData turretdata, string animboolname) : base(controller, statemachine, turretdata, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        turretController.gunController.TryStartRotationAndAim();
        turretController.StartAiming();
    }

    public override void Exit()
    {
        base.Exit();
        turretController.StopAiming();
        turretController.gunController.StopRotationAndAim();
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

