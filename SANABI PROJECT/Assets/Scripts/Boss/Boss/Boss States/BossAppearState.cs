using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearState : BossState
{
    private Vector2 appearDirection = Vector2.right + Vector2.down;
    private Vector2 appearVelocity;
    private float appearSpeed;
    public BossAppearState(BossController bossController, BossStateMachine bossStateMachine, BossData bossData, string animBoolName) : base(bossController, bossStateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        appearSpeed = bossData.appearSpeed;
    }

    public override void Enter()
    {
        base.Enter();
        //bossController.transform.position = bossController.bossSpawnSpot.position;
        appearVelocity = appearDirection * appearSpeed;
        bossController.SetBossVelocity(appearVelocity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (bossController.CheckIfGrounded())
        {
            GameManager.Instance.playerController.TurnOnGetHitCamShake();
            stateMachine.ChangeState(bossController.IdleState);
        }
    }
}
