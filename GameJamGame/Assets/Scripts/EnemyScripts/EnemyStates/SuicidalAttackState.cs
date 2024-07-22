using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class SuicidalAttackState : IState
{
    private readonly StandardEnemy _character;
    private float _attackForce;
    public SuicidalAttackState(StandardEnemy character, float attackForce)
    {
        _character = character;
        _attackForce = attackForce;
    }

    public void OnEnter()
    {
        _character.NavAgent.isStopped = true;
        _character.NavAgent.enabled = false;

        _character.Rb.isKinematic = false;
        _character.Rb.useGravity = true;

        //Ram into the target via rigid body
        BoostIntoTarget(_character.Target.transform, _character.Rb, _attackForce);
    }

    public void OnExit()
    {
        _character.NavAgent.enabled = true;
        _character.NavAgent.isStopped = false;

        _character.Rb.isKinematic = true;
        _character.Rb.useGravity = false;
    }

    public void Tick()
    {
        //NA
    }

    private void BoostIntoTarget(Transform target, Rigidbody rb, float boostForce)
    {
        //Calculate Direction
        Vector3 direction = target.position - rb.position;
        direction = Vector3.Normalize(direction);

        rb.AddForce(direction * boostForce, ForceMode.Impulse);
    }

    private void DisableRB()
    {

    }
}
