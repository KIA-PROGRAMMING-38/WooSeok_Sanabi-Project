using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class GrabReturningState : GrabState
{
    private Vector3 distance;
    private float angle;
    public GrabReturningState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName) : base(grab, grabStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetGrabStatus();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        GrabReturn();
        GrabRotate();

        if (grab.CheckIfGrabReturned())
        {
            grabStateMachine.ChangeState(grab.IdleState);
        }
    }

    private void GrabReturn()
    {
        grab.transform.position = Vector2.MoveTowards(grab.transform.position, grab.GrabReturnCollider.transform.position, playerData.shootSpeed * Time.deltaTime);
    }

    
    private void GrabRotate()
    {
        distance = grab.transform.position - grab.GrabReturnCollider.transform.position;
        angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        grab.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    public override void Exit()
    {
        base.Exit();
        grab.IsGrabReturned = true;
        SetStartPos();
    }

    private void SetStartPos()
    {
        grab.startPos = grab.GrabReturnCollider.transform.position;
    }

    private void SetGrabStatus()
    {
        grab.trailRenderer.enabled = false;
        grab.GrabReturnCollider.enabled = true;
        grab.grabRigid.velocity = Vector3.zero;
    }
}
