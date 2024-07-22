using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : IState
{
    private readonly StandardEnemy _character;

    public PursueState(StandardEnemy character)
    {
        _character = character;
    }

    public void OnEnter()
    {
        _character.NavAgent.isStopped = false;
        _character.NavAgent.speed = _character.PursuitSpeed;
    }

    public void OnExit()
    {
        // NA
    }

    public void Tick()
    {
        _character.NavAgent.SetDestination(_character.Target.transform.position);
    }
}
