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
        grabController.ConvertMouseInput(false);
        grabController.playerInput.UseWireShoot();
        grabController.IsFlying = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (grabController.HitNormal)
        {
            stateMachine.ChangeState(grabController.GrabbedState);
        }
        else if (grabController.CheckIfTooFar() || grabController.HitNoGrab)
        {
            stateMachine.ChangeState(grabController.ReturningState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void SetVariables()
    {
        grabController.IsFlying = true;
        grabController.isGrappled = false;
        grabController.IsGrabReturned = false;
        grabController.HitNoGrab = false;
        grabController.HitNormal = false;
        grabController.GrabReturnCollider.enabled = false;
        grabController.capsuleCollider.enabled = true;
    }

    private void SetGrabStatus()
    {
        grabController.grabRigid.bodyType = RigidbodyType2D.Dynamic;
        grabController.grabRigid.gravityScale = 0f;
        grabController.spriteRenderer.enabled = true;
        grabController.trailRenderer.emitting = true;
    }
    private void ShootGrab()
    {
        SetGrabStatus();
        grabController.grabRigid.velocity = grabController.transform.up * playerData.shootSpeed;
    }
}
