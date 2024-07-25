using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class AttackState : IState
{
    private readonly EnemyBase _character;
    private readonly AttackBase _attack;

    public AttackState(EnemyBase character, AttackBase attack)
    {
        _character = character;
        _attack = attack;
    }

    public void OnEnter()
    {
        _attack.EnterAttack();
    }

    public void OnExit()
    {
        _attack.ExitAttack();
    }

    public void Tick()
    {
        if (_attack.CanAttack())
        {
            _attack.DoAttack(_character.Target.transform);
        }
    }
}
