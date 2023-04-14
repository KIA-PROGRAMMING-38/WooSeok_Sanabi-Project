using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    private int maxHP;
    private int currentHP;
    public virtual void Die()
    {
        
    }

    public virtual void SetInvincible()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        
    }

    public virtual void ResetToMaxHP()
    {

    }

    public virtual int GetCurrentHp()
    {
        return currentHP;
    }

    public virtual int GetMaxHp()
    {
        return maxHP;
    }

    public virtual void RecoverHP()
    {

    }
}
