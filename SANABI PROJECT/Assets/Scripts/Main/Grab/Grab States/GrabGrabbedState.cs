using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabGrabbedState : GrabState
{
    Vector2 holdPosition;
    Quaternion holdRotation;
    private bool mouseInputStop;
    public GrabGrabbedState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName) : base(grab, grabStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        // holdPosition = grab.transform.position;
        // holdRotation = grab.transform.rotation;
        grab.isGrappled = true;
        grab.grabRigid.bodyType = RigidbodyType2D.Static;
    }

    public override void Exit()
    {
        base.Exit();
        grab.grabRigid.bodyType = RigidbodyType2D.Dynamic;
        grab.isGrappled = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        mouseInputStop = grab.playerInput.MouseInputStop;
        if (mouseInputStop)
        {
            grabStateMachine.ChangeState(grab.ReturningState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
