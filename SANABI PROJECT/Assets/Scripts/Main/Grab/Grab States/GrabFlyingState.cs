using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFlyingState : GrabState
{
    public GrabFlyingState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName) : base(grab, grabStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        grab.isGrabReturned = false;
        grab.HitNoGrab = false;
        grab.HitNormal = false;
        grab.FlyGrab();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        grab.transform.rotation = grab.flyRotation; // i really didn't want to do this, but it keeps rotating after collision with nograp wall
        if (grab.HitNormal)
        {
            grabStateMachine.ChangeState(grab.GrabbedState);
        }
        else if (grab.CheckIfTooFar() || grab.HitNoGrab)
        {
            grabStateMachine.ChangeState(grab.ReturningState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
