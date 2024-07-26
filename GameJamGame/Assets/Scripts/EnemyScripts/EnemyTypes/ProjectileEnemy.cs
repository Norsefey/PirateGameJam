using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : EnemyBase
{
    [SerializeField] ProjectileAttack _attack;

    protected override void Awake()
    {
        base.Awake();

        #region State Machine Setup
        // Create the states
        var idle = new IdleState(this);
        var pursue = new PursueState(this);
        var projectileAttack = new AttackState(this, _attack);
        var die = new DieState(this);

        // Normal Transitions
        At(idle, pursue, new Func<bool>[] { HasTarget });
        At(idle, projectileAttack, new Func<bool>[] { HasTarget, TargetInRange });

        At(pursue, projectileAttack, new Func<bool>[] { HasTarget, TargetInRange });

        At(projectileAttack, pursue, new Func<bool>[] { Or(Not(TargetInRange), Not(HasTarget)) });

        // Any Transitions
        _stateMachine.AddAnyTransition(die, new Func<bool>[] { IsDead });
        _stateMachine.AddAnyTransition(idle, new Func<bool>[] { Not(HasTarget) });

        //Set the default state
        _stateMachine.SetState(idle);
        #endregion
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
