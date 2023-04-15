using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretState
{
    protected TurretController turretController;
    protected TurretStateMachine stateMachine;
    protected TurretData turretData;
    private string animBoolName;

    public TurretState(TurretController controller, TurretStateMachine statemachine, TurretData turretdata, string animboolname)
    {
        this.turretController = controller;
        this.stateMachine = statemachine;
        this.turretData = turretdata;
        this.animBoolName = animboolname;
    }

    public virtual void DoChecks()
    {

    }

    public virtual void Enter()
    {
        DoChecks();
        turretController.Animator.SetBool(animBoolName, true);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void Exit()
    {
        turretController.Animator.SetBool(animBoolName, false);
    }
}
