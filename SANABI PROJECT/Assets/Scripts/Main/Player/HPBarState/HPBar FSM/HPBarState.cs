using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarState 
{
    protected HPBarController hpBarController;
    protected HPBarStateMachine stateMachine;
    protected PlayerHealth playerHealth;

    protected string animBoolName;
    protected float startTime;
    protected bool isExitingState;
    protected bool isPlayerDamaged;
    protected int playerMaxHP;
    protected int playerCurrentHP;
    protected string playerHPName = "playerHP";
    
    public HPBarState(HPBarController follow, HPBarStateMachine statemachine, PlayerHealth playerHealth, string animboolname)
    {
        this.hpBarController = follow;
        this.stateMachine = statemachine;
        this.playerHealth = playerHealth;
        this.animBoolName = animboolname;

        this.playerHealth.OnChangedHP -= UpdateHP;
        this.playerHealth.OnChangedHP += UpdateHP;
        //this.playerHealth.OnIdleHP -= ResetHP;
        //this.playerHealth.OnIdleHP += ResetHP;
    }

    public virtual void Enter()
    {
        DoChecks();
        
        hpBarController.animator.SetBool(animBoolName, true);
        startTime = Time.time;
        isExitingState = false;
        //playerHealth.OnChangedHP -= UpdateHP;
        //playerHealth.OnChangedHP += UpdateHP;
        
    }

    public virtual void Exit()
    {
        hpBarController.animator.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        isPlayerDamaged = hpBarController.IsPlayerDamaged;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
        
        playerCurrentHP = playerHealth.GetCurrentHp();
        playerMaxHP = playerHealth.GetMaxHp();
    }

    public void UpdateHP(int hp)
    {
        hpBarController.animator.SetInteger(playerHPName, hp);
    }

    private void ResetHP(int playerMaxHp)
    {
        hpBarController.animator.SetInteger(playerHPName, playerMaxHp);
    }
}
