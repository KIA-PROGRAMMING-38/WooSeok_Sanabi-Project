using System.Collections;
using System.Collections.Generic;
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
        grabController.IgnoreTurretCollision(true);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        GrabReturn();
        GrabRotate();

        if (grabController.CheckIfGrabReturned())
        {
            stateMachine.ChangeState(grabController.IdleState);
        }
    }

    private void GrabReturn()
    {
        grabController.transform.position = Vector2.MoveTowards(grabController.transform.position, grabController.GrabReturnCollider.transform.position, playerData.shootSpeed * 2f * Time.deltaTime);
    }

    
    private void GrabRotate()
    {
        distance = grabController.transform.position - grabController.GrabReturnCollider.transform.position;
        angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        grabController.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    public override void Exit()
    {
        base.Exit();
        grabController.IsGrabReturned = true;
        SetStartPos();
        grabController.IgnoreTurretCollision(false);
    }

    private void SetStartPos()
    {
        grabController.startPos = grabController.GrabReturnCollider.transform.position;
    }

    private void SetGrabStatus()
    {
        grabController.trailRenderer.emitting= false;
        grabController.GrabReturnCollider.enabled = true;
        grabController.grabRigid.velocity = Vector3.zero;
    }
}
