using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRobotState 
{
    protected HPRobotController hpRobotController;
    protected HPRobotStateMachine stateMachine;
    protected PlayerHealth playerHealth;

    protected string animBoolName;
    protected float startTime;

    protected float damageEnterTime;
    protected bool isExitingState;
    protected bool isPlayerDamaged;
    protected bool isPlayerDead;

    protected int playerMaxHP;
    protected int playerCurrentHP;
    protected string playerHPName = "playerHP";
    
    public HPRobotState(HPRobotController follow, HPRobotStateMachine statemachine, PlayerHealth playerHealth, string animboolname)
    {
        this.hpRobotController = follow;
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
        
        hpRobotController.animator.SetBool(animBoolName, true);
        startTime = Time.time;
        isExitingState = false;
        //playerHealth.OnChangedHP -= UpdateHP;
        //playerHealth.OnChangedHP += UpdateHP;
        
    }

    public virtual void Exit()
    {
        hpRobotController.animator.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        isPlayerDamaged = hpRobotController.IsPlayerDamaged;
        isPlayerDead = playerHealth.CheckIfDead();
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
        hpRobotController.animator.SetInteger(playerHPName, hp);
    }

    private void ResetHP(int playerMaxHp)
    {
        hpRobotController.animator.SetInteger(playerHPName, playerMaxHp);
    }
}
