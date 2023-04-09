using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireGrappledState : PlayerWireState
{
    // joint를 연결해줘야함
    Vector2 playerArmPos;
    Vector2 hitTargetPos;
    public PlayerWireGrappledState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        playerArmPos = player.WireController.gameObject.transform.position;
        hitTargetPos = player.WireController._hitTarget.point;
        player.Joint.enabled = true;
        player.Joint.distance = Vector2.Distance(hitTargetPos, playerArmPos);
        player.Joint.connectedAnchor = hitTargetPos;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.AddXVelocityWhenGrappled(player.Input.MovementInput.x);

        if (player.Input.MouseInputStop)
        {
            player.Joint.enabled = false;
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
