using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TurretExecuteHoldedState : TurretState
{
    public TurretExecuteHoldedState(TurretController controller, TurretStateMachine statemachine, TurretData turretdata, string animboolname) : base(controller, statemachine, turretdata, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        turretController.playerController.OffTurret -= ChangeToDeadState;
        turretController.playerController.OffTurret += ChangeToDeadState;
    }

    public override void Exit()
    {
        base.Exit();
        turretController.playerController.OffTurret -= ChangeToDeadState;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChangeToDeadState()
    {
        stateMachine.ChangeState(turretController.DeadState);
    }
}

