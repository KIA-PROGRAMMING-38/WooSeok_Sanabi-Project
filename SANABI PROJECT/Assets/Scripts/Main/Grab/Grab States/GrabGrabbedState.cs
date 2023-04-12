using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabGrabbedState : GrabState
{
    Vector2 holdPosition;
    Quaternion holdRotation;
    public GrabGrabbedState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName) : base(grab, grabStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GetPosAndRot();
        grab.AnchorPosition = holdPosition;
        grab.isGrappled = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldGrab();
        if (!mouseInputHold)
        {
            grabStateMachine.ChangeState(grab.ReturningState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        grab.isGrappled = false;
    }
    private void GetPosAndRot()
    {
        holdPosition = grab.transform.position;
        holdRotation = grab.transform.rotation;
    }

    private void HoldGrab()
    {
        grab.transform.position = holdPosition;
        grab.transform.rotation= holdRotation;
    }
}
