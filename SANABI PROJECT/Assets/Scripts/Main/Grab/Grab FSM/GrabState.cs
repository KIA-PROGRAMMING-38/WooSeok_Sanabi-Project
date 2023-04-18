using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabState
{
    protected GrabController grabController;
    protected GrabStateMachine stateMachine;
    protected PlayerData playerData;
    private string animBoolName;
    protected bool mouseInput;
    protected bool mouseInputHold;

    public GrabState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName)
    {
        this.grabController = grab;
        this.stateMachine = grabStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        grabController.OnGrabTurret -= ChangeToGrabIdleState;
        grabController.OnGrabTurret += ChangeToGrabIdleState;
    }

    public virtual void Enter()
    {
        DoChecks();
        grabController.Animator.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        grabController.Animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        mouseInputHold = grabController.playerInput.MouseInputHold;
        mouseInput = grabController.playerInput.MouseInput;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    private void ChangeToGrabIdleState()
    {
        stateMachine.ChangeState(grabController.IdleState);
    }
}
