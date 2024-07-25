using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : IState
{
    private readonly EnemyBase _character;

    public PursueState(EnemyBase character)
    {
        _character = character;
    }

    public void OnEnter()
    {
        _character.NavAgent.ResetPath();
        _character.NavAgent.isStopped = false;
        _character.NavAgent.speed = _character.PursuitSpeed;
    }

    public void OnExit()
    {
        _character.NavAgent.ResetPath();
    }

    public void Tick()
    {
        _character.NavAgent.SetDestination(_character.Target.transform.position);
    }
}
