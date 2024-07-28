using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    private readonly EnemyBase _character;

    public DieState(EnemyBase character)
    {
        _character = character;
    }
    public void OnEnter()
    {
        _character.NavAgent.isStopped = true;
        _character.Die();
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
