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
        turretController.BodyAnimator.SetBool(animBoolName, true);
        turretController.GunAnimator.SetBool(animBoolName, true);
        turretController.StageAnimator.SetBool(animBoolName, true);

        turretController.grabController.OnGrabTurret -= ChangeToTurretExecuteHoldedState;
        turretController.grabController.OnGrabTurret += ChangeToTurretExecuteHoldedState;
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
        turretController.BodyAnimator.SetBool(animBoolName, false);
        turretController.GunAnimator.SetBool(animBoolName, false);
        //turretController.StageAnimator.SetBool(animBoolName, false); // no need to falsify it

        turretController.grabController.OnGrabTurret -= ChangeToTurretExecuteHoldedState;
    }

    private void ChangeToTurretExecuteHoldedState()
    {
        stateMachine.ChangeState(turretController.ExecuteHoldedState);
    }
}
