using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    protected EnemyBase _character;
    public event Action OnAttack;
    protected bool bCanAttack = true;

    private void Awake()
    {
        _character = GetComponent<EnemyBase>();
    }

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

    protected virtual bool Aim(Transform target, float rotationSpeed)
    {
        Vector3 direction = (target.position - this.gameObject.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if (Vector3.Dot(this.gameObject.transform.forward, direction) > 0.995f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
