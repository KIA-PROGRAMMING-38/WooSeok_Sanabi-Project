using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabReturningState : GrabState
{
    public GrabReturningState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName) : base(grab, grabStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        grab.ReturnGrab();
        grab.GrabReturnCollider.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        grab.IsGrabReturned = true;
        grab.startPos = grab.GrabReturnCollider.transform.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        grab.GrabChaseSNB();
        if (grab.CheckIfReturned())
        {
            grabStateMachine.ChangeState(grab.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
