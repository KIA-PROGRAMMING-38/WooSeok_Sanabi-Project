using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GrabIdleState : GrabState
{
    private Vector2 distanceVector;
    private float angle;
    public GrabIdleState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName) : base(grab, grabStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetGrab();
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        GrabRotateBasedOnCursor();
        GrabAttachedToBody();
        
        if (grab.CheckIfMouseInput())
        {
            grabStateMachine.ChangeState(grab.FlyingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void ResetGrab()
    {
        grab.capsuleCollider.enabled = false;
        grab.GrabReturnCollider.enabled = false;
        grab.spriteRenderer.enabled = false;
        grab.grabRigid.velocity = Vector2.zero;
        grab.transform.position = grab.startPos;
        grab.grabRigid.bodyType = RigidbodyType2D.Kinematic;
    }

    private void GrabAttachedToBody()
    {
        grab.transform.position = grab.playerTransform.position;
    }
    private void GrabRotateBasedOnCursor()
    {
        distanceVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - grab.transform.position;
        angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        grab.transform.rotation = Quaternion.Euler(0f, 0f, angle + 270f);;
    }
}
