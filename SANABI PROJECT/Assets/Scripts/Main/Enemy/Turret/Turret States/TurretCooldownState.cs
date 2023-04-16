using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCooldownState : TurretState
{
    public TurretCooldownState(TurretController controller, TurretStateMachine statemachine, TurretData turretdata, string animboolname) : base(controller, statemachine, turretdata, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        turretController.OnFinishedCooldown -= ChangeToAimingState;
        turretController.OnFinishedCooldown += ChangeToAimingState;
        turretController.WaitUntilCooldown();
    }

    public override void Exit()
    {
        base.Exit();
        turretController.StopCooldown();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChangeToAimingState()
    {
        stateMachine.ChangeState(turretController.AimingState);
    }
}
