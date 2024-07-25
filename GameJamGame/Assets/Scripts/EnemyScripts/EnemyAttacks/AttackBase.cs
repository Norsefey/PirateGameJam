using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    public event Action OnAttack;
    protected bool bCanAttack = true;
    
    public virtual void DoAttack(Transform target)
    {
        OnAttack?.Invoke();
    }

    public virtual bool CanAttack()
    {
        return bCanAttack;
    }

    /// <summary>
    /// Initialize the attack
    /// </summary>
    public abstract void EnterAttack();

    /// <summary>
    /// Cleanup after the attack is done
    /// </summary>
    public abstract void ExitAttack();

    protected IEnumerator Cooldown(float cooldownTime)
    {
        bCanAttack = false;
        yield return new WaitForSeconds(cooldownTime);
        bCanAttack = true;
    }
}
