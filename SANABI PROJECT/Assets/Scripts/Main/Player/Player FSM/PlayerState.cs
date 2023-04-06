using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected float startTime; // defines how long the player has been in the state
    private string animBoolName;
    protected bool isExitingState; // to prevent executing scripts inbetween state changes inside the same SuperStates.
    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine= stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() // when enter the state
    {
        DoChecks();
        player.BodyAnimator.SetBool(animBoolName, true);
        player.ArmAnimator.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit() // when exit the state
    {
        player.BodyAnimator.SetBool(animBoolName, false);
        player.ArmAnimator.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate() // update for each frame
    {

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
    
}
