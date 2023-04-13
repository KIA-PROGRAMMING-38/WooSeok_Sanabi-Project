using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IDamagable
{

    public void SetInvincible();
    public void Die();
    public void TakeDamage(int damage);

}

