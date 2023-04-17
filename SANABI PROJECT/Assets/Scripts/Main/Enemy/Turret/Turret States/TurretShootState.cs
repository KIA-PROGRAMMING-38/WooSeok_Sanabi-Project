using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TurretShootState : TurretState
{
    public TurretShootState(TurretController controller, TurretStateMachine statemachine, TurretData turretdata, string animboolname) : base(controller, statemachine, turretdata, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        turretController.OnFinishedShooting -= ChangeToCooldownState;
        turretController.OnFinishedShooting += ChangeToCooldownState;
        turretController.StartShooting();
    }

    public override void Exit()
    {
        base.Exit();
        turretController.StopShooting();
    }

    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChangeToCooldownState()
    {
        stateMachine.ChangeState(turretController.CooldownState);
    }
}

