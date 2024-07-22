using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidalEnemy : StandardEnemy
{
    [SerializeField] float rammingForce;

    protected override void Awake()
    {
        base.Awake();

        // Create the states
        var pursue = new PursueState(this);
        var suicidalAttack = new SuicidalAttackState(this, rammingForce);
        var die = new DieState(this);

        // At() is the shorthand of stateMachine.AddTransition
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        // Normal Transitions
        At(pursue, suicidalAttack, HasTargetInRange());
        At(suicidalAttack, pursue, HasTargetOutRange());

        // Any Transitions
        _stateMachine.AddAnyTransition(die, IsDead());

        //Set the default state
        _stateMachine.SetState(pursue);

        // Conditions
        Func<bool> HasTargetInRange() => () => Target != null && Vector3.Distance(transform.position, Target.transform.position) <= _attackRange;
        Func<bool> HasTargetOutRange() => () => Target != null && Vector3.Distance(transform.position, Target.transform.position) > _attackRange;
        Func<bool> IsDead() => () => _isDead == true;
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
