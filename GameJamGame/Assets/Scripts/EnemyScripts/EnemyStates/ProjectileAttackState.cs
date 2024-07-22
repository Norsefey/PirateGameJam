using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileAttackState : IState
{
    private readonly ProjectileEnemy _character;

    public ProjectileAttackState(ProjectileEnemy character)
    {
        _character = character;
    }

    public void OnEnter()
    {
        //Slow down to avoid reaching the target
        _character.NavAgent.speed = _character.AvoidanceSpeed;

    }

    public void OnExit()
    {
        //
    }

    public void Tick()
    {
        //Shoot
        if (_character.CanShoot)
        {
            _character.Shoot(_character.Target.transform);
        }

        

    }

    private void GetDestinationNearTarget(Transform target)
    {
        Vector3 destination = target.position;
    }


}
