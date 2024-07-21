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
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }
}
