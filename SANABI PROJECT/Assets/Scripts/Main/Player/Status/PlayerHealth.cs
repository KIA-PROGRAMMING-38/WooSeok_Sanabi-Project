using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : Health
{
    [SerializeField] private PlayerData playerData;
    
    private int playerMaxHP;
    private int playerCurHP;
    public float recoveryCooltime = 8f;
    public int recoverStrength;
    public float transitionCooltime = 3f;
    public float invicibleTime;
    private bool isPlayerInvincible;

    public event Action<int> OnChangedHP;
    public event Action<int> OnResetHP;
    public event Action<int> OnRecoverHP;

    private void OnEnable()
    {
        playerCurHP = playerMaxHP = playerData.playerHP;
        invicibleTime = playerData.invincibleTime;
        recoverStrength = playerData.PlayerHPRecoverStrength;
    }

    private void Update()
    {
        
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
