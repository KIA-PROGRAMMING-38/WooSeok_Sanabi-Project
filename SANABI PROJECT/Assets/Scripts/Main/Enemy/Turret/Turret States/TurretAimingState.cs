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
        turretController.WarningInsideAnimator.SetBool("warningIdle", false);
        turretController.WarningInsideAnimator.SetBool("warningWhile", true);
        turretController.WarningOutsideAnimator.SetBool("warningIdle", false);
        turretController.WarningOutsideAnimator.SetBool("warningWhile", true);

    }

    public override void Exit()
    {
        base.Exit();
        turretController.gunController.StopRotationAndAim();
        turretController.StopAiming();
        turretController.WarningInsideAnimator.SetBool("warningWhile", false);
        turretController.WarningOutsideAnimator.SetBool("warningWhile", false);


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

