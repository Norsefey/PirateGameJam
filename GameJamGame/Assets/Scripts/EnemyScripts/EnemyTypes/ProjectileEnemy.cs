using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : StandardEnemy
{
    [SerializeField] float projectileSpeed;
    [SerializeField] Transform projectileStart;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float cooldownTime;

    public bool CanShoot = true;

    protected override void Awake()
    {
        base.Awake();

        // Create the states
        var pursue = new PursueState(this);
        var projectileAttack = new ProjectileAttackState(this);
        var die = new DieState(this);

        // At() is the shorthand of stateMachine.AddTransition
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        // Normal Transitions
        At(pursue, projectileAttack, HasTargetInRange());
        At(projectileAttack, pursue, HasTargetOutRange());

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

    public void Shoot(Transform target)
    {
        GameObject projectileObj = Instantiate(projectilePrefab, projectileStart.position, Quaternion.identity);
        projectileObj.TryGetComponent<Projectile>(out Projectile projectile);
        Vector3 direction = target.position - projectileStart.position;

        projectile.Initialize(5, projectileSpeed, direction);

        StartCoroutine(Cooldown(cooldownTime));
    }

    private IEnumerator Cooldown(float coolDownTime)
    {
        CanShoot = false;
        yield return new WaitForSeconds(coolDownTime);
        CanShoot = true;
    }
}
