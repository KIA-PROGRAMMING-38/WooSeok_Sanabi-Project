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
    public float transitionCooltime = 3f;
    private bool isPlayerInvincible;

    public event Action<int> OnChangedHP;
    public event Action<int> OnIdleHP;

    private void OnEnable()
    {
        playerCurHP = playerMaxHP = playerData.playerHP;
        OnIdleHP?.Invoke(playerMaxHP);
        
    }

    private void Update()
    {
        
    }


    public override void TakeDamage(int damage)
    {
        playerCurHP -= damage;
        OnChangedHP?.Invoke(playerCurHP);
        
    }

    public override void Die()
    {
       
    }
    public override void ResetToMaxHP()
    {
        playerCurHP = playerMaxHP;
        OnIdleHP?.Invoke(playerMaxHP);
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
