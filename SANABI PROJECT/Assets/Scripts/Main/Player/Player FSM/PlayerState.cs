using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerController playerController;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected float startTime; // defines how long the player has been in the state
    private string animBoolName;
    protected bool isExitingState; // to prevent executing scripts inbetween state changes inside the same SuperStates.

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.playerController = player;
        this.stateMachine= stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        //playerController.GrabController.OnGrabTurret -= ChangeToApproachDashState;
        //playerController.GrabController.OnGrabTurret += ChangeToApproachDashState;
        
    }

    public virtual void Enter() // when enter the state
    {
        DoChecks();
        playerController.BodyAnimator.SetBool(animBoolName, true);
        playerController.ArmAnimator.SetBool(animBoolName, true);
        playerController.playerHealth.OnDead -= ChangeToDeadState;
        playerController.playerHealth.OnDead += ChangeToDeadState;
        playerController.GrabController.OnGrabTurret -= ChangeToApproachDashState;
        playerController.GrabController.OnGrabTurret += ChangeToApproachDashState;
        GameManager.Instance.grabController.OnGrabBoss -= ChangeToApproachDashState;
        GameManager.Instance.grabController.OnGrabBoss += ChangeToApproachDashState;
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit() // when exit the state
    {
        playerController.BodyAnimator.SetBool(animBoolName, false);
        playerController.ArmAnimator.SetBool(animBoolName, false);
        playerController.playerHealth.OnDead -= ChangeToDeadState;
        playerController.GrabController.OnGrabTurret -= ChangeToApproachDashState;
        GameManager.Instance.grabController.OnGrabBoss -= ChangeToApproachDashState;
        isExitingState = true;
    }

    public virtual void LogicUpdate() // update for each frame
    {
        //playerController.AfterImage();
        playerController.SetMinMaxVelocityY();
    }

    public virtual void PhysicsUpdate() // update for fixed time
    {
        DoChecks();
    }

    public virtual void DoChecks() // called from PhysicsUpdate & Enter
    {

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    
    private void ChangeToApproachDashState()
    {
        stateMachine.ChangeState(playerController.ApproachDash);
    }

    private void ChangeToDeadState()
    {
        stateMachine.ChangeState(playerController.DeadState);
    }
}
