using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : Health
{
    private int playerMaxHP;
    private int playerCurHP;
    private bool isPlayerInvincible;

    public PlayerHealth(int playerHPSet)
    {
        playerCurHP = playerMaxHP = playerHPSet;
    }


    public override void TakeDamage(int damage)
    {
        playerCurHP -= damage;

    }

    public override void Die()
    {
        
    }

    public override void SetInvincible()
    {
        base.SetInvincible();
        if (isPlayerInvincible)
        {

        }
        else
        {

        }

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
