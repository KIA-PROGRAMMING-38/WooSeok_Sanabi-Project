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
        grabController.AnchorPosition = holdPosition;
        grabController.isGrappled = true;
        //HoldGrab();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldGrab();
        //GameManager.Instance.armController.ConnectAnchor();
        if (!mouseInputHold)
        {
            //GameManager.Instance.armController.DisconnectAnchor();
            stateMachine.ChangeState(grabController.ReturningState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        grabController.isGrappled = false;
    }
    private void GetPosAndRot()
    {
        holdPosition = grabController.transform.position;
        holdRotation = grabController.transform.rotation;
    }

    private void HoldGrab()
    {
        grabController.transform.position = holdPosition;
        grabController.transform.rotation = holdRotation;
    }
}
