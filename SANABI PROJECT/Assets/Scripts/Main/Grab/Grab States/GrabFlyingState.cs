using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFlyingState : GrabState
{
    public GrabFlyingState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName) : base(grab, grabStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetVariables();
        ShootGrab();
    }
    
    public override void Exit()
    {
        base.Exit();
        grab.ConvertMouseInput(false);
        grab.playerInput.UseWireShoot();
        grab.IsFlying = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

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
    private void SetVariables()
    {
        grab.IsFlying = true;
        grab.isGrappled = false;
        grab.IsGrabReturned = false;
        grab.HitNoGrab = false;
        grab.HitNormal = false;
        grab.GrabReturnCollider.enabled = false;
        grab.capsuleCollider.enabled = true;
    }

    private void SetGrabStatus()
    {
        grab.grabRigid.bodyType = RigidbodyType2D.Dynamic;
        grab.grabRigid.gravityScale = 0f;
        grab.spriteRenderer.enabled = true;
        grab.trailRenderer.emitting = true;
    }
    private void ShootGrab()
    {
        SetGrabStatus();
        grab.grabRigid.velocity = grab.transform.up * playerData.shootSpeed;
    }
}
