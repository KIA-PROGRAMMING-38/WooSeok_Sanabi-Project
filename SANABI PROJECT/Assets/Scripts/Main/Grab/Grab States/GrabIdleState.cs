using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabIdleState : GrabState
{
    private bool MouseInput;
    public GrabIdleState(GrabController grab, GrabStateMachine grabStateMachine, PlayerData playerData, string animBoolName) : base(grab, grabStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        grab.ResetGrab();
        grab.DeactivateGrab();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MouseInput = grab.playerInput.MouseInput;
        grab.transform.rotation = Quaternion.Euler(0f, 0f, grab.playerController.WireController.angle + 270f);
        if (MouseInput)
        {
            grabStateMachine.ChangeState(grab.FlyingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
