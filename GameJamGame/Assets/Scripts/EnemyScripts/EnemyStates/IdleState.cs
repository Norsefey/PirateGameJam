using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private readonly EnemyBase _character;
    public IdleState(EnemyBase character)
    {
        _character = character;
    }

    public void OnEnter()
    {
        _character.NavAgent.isStopped = true;
        _character.NavAgent.ResetPath();
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
