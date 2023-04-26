using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : Health
{
    //[SerializeField] private PlayerData playerData;
    private PlayerData playerData;
    private PlayerController playerController;

    private int playerMaxHP;
    private int playerCurHP;

    public float recoveryCooltime = 8f;
    public int recoverStrength;

    public float transitionCooltime = 3f;
    public float invicibleTime;
    private bool isPlayerInvincible;

    private bool isPlayerDead;

    public event Action<int> OnChangedHP;
    public event Action<int> OnResetHP;
    public event Action<int> OnRecoverHP;
    public event Action OnDead;

    private void Awake()
    {
        playerData = GameManager.Instance.playerData;
    }

    private void OnEnable()
    {
        //playerData = GameManager.Instance.playerData;
        playerController = GameManager.Instance.playerController;
        playerCurHP = playerMaxHP = playerData.playerHP;
        invicibleTime = playerData.damagedOutTime;
        recoverStrength = playerData.PlayerHPRecoverStrength;
    }

    private void Update()
    {
        if (playerCurHP <= 0 || playerController.CheckIfPlayerHitDeathPlatform())
        {
            InvokeOnDead();
            isPlayerDead = true;
            //OnDead?.Invoke();
        }
    }

    private void InvokeOnDead()
    {
        if (!isPlayerDead)
        {
            OnDead?.Invoke();
        }
        
    }

    public override bool CheckIfDead()
    {
        return isPlayerDead;
    }
    public override void TakeDamage(int damage)
    {
        playerCurHP -= damage;
        OnChangedHP?.Invoke(playerCurHP);
        
    }
    public override void RecoverHP()
    {
        playerCurHP += recoverStrength;
        OnRecoverHP?.Invoke(playerCurHP);
    }

    

    public override void Die()
    {
       
    }
    public override void ResetToMaxHP()
    {
        playerCurHP = playerMaxHP;
        OnResetHP?.Invoke(playerMaxHP);
    }
    public override int GetCurrentHp()
    {
        return playerCurHP;
    }
    
    public override int GetMaxHp()
    {
        return playerMaxHP;
    }
}
