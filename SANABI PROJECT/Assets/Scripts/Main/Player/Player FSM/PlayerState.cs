using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime; // defines how long the player has been in the state
    private string animBoolName;
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
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
    }

    public virtual void Exit() // when exit the state
    {
        player.BodyAnimator.SetBool(animBoolName, false);
        player.ArmAnimator.SetBool(animBoolName, false);
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

}
