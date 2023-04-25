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
        
        
        if (mouseInput && !GameManager.Instance.playerController.isPlayerBossState)
        {
            stateMachine.ChangeState(grabController.FlyingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void ResetGrab()
    {
        grabController.capsuleCollider.enabled = false;
        grabController.GrabReturnCollider.enabled = false;
        grabController.spriteRenderer.enabled = false;
        grabController.grabRigid.velocity = Vector2.zero;
        grabController.transform.position = grabController.startPos;
        grabController.grabRigid.bodyType = RigidbodyType2D.Kinematic;
        grabController.trailRenderer.emitting= false;
    }

    private void GrabAttachedToBody()
    {
        grabController.transform.position = grabController.playerTransform.position;
    }
    private void GrabRotateBasedOnCursor()
    {
        distanceVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - grabController.transform.position;
        angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        grabController.transform.rotation = Quaternion.Euler(0f, 0f, angle + 270f);;
    }
}
