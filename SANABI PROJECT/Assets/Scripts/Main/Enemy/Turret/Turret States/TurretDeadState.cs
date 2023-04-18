using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TurretDeadState : TurretState
{
    public TurretDeadState(TurretController controller, TurretStateMachine statemachine, TurretData turretdata, string animboolname) : base(controller, statemachine, turretdata, animboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        turretController.onTurretDeath -= TellTurretSpawnerIAmDead;
        turretController.onTurretDeath += TellTurretSpawnerIAmDead;
        turretController.DisableCollider();
        turretController.SpreadBrokenParts();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void TellTurretSpawnerIAmDead()
    {
        turretController.turretSpawner.AddTurretDeathCount();
    }
}

