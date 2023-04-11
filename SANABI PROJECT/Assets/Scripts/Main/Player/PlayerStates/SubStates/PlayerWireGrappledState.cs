using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWireGrappledState : PlayerWireState
{
    // joint를 연결해줘야함
    private Vector2 playerArmPos;
    private Vector2 hitTargetPos;
    private Vector2 ArmRotateDistance;
    private float ArmAngle;
    private bool MouseHoldInput;

    private bool DashInput;
    

    public PlayerWireGrappledState(SNBController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        ConnectAnchor();
    }

    

    public override void Exit()
    {
        base.Exit();
        player.armTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player.PlayerWireDashStop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.AddXVelocityWhenGrappled(player.Input.MovementInput.x);

        ArmRotateTowardsAnchor();

        

        MouseHoldInput = player.Input.MouseInputHold;
        DashInput = player.Input.DashInput;
        
        if (DashInput)
        {
            player.PlayerWireDash();
        }

        player.AfterImage();

        if (!MouseHoldInput)
        {
            player.Joint.enabled = false;
            if (isGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (!isGrounded)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void ArmRotateTowardsAnchor()
    {
        playerArmPos = player.WireController.gameObject.transform.position;
        ArmRotateDistance = hitTargetPos - playerArmPos;
        ArmAngle = Mathf.Atan2(ArmRotateDistance.y, ArmRotateDistance.x) * Mathf.Rad2Deg;
        player.armTransform.rotation = Quaternion.Euler(0f, 0f, ArmAngle);
    }
    private void ConnectAnchor()
    {
        playerArmPos = player.WireController.gameObject.transform.position;
        hitTargetPos = player.WireController._hitTarget.point;
        player.Joint.enabled = true;
        player.Joint.distance = Vector2.Distance(hitTargetPos, playerArmPos);
        player.Joint.connectedAnchor = hitTargetPos;
    }
    
}
