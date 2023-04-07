using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabState 
{
    protected GrabController grab;
    protected GrabStateMachine grabStateMachine;
    protected PlayerData playerData;
    private string animBoolName;

    public GrabState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName)
    {
        this.grab = grab;
        this.grabStateMachine = grabStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        grab.Animator.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        grab.Animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

}
