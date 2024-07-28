using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ProjectileAttack : AttackBase
{
    [SerializeField] float _cooldownTime;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _minDistanceToTarget;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _projectileStart;
    [SerializeField] float _attackMoveSpeed; //Speed of the enemy during the attack state

    public override void DoAttack(Transform target)
    {
        base.DoAttack(target);

        if(Vector3.Distance(transform.position, target.position) < _minDistanceToTarget)
        {
            _character.NavAgent.SetDestination(GetPositionNearPlayer(target));
        }

        bool isAimed = Aim(target, _rotationSpeed);
        if (!isAimed) return;
        Shoot(target, _projectilePrefab, _projectileStart);
        StartCoroutine(Cooldown(_cooldownTime));
    }

    public override void EnterAttack()
    {
        _character.NavAgent.speed = _attackMoveSpeed;
        _character.NavAgent.ResetPath();
    }

    private Vector3 GetPositionNearPlayer(Transform target)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * _minDistanceToTarget;
        Vector3 position = new Vector3(randomPoint.x, 0, randomPoint.y) + target.position;

        if(Terrain.activeTerrain.TryGetComponent<Terrain>(out Terrain terrain))
            position.y = terrain.SampleHeight(position);

        return position;
    }

    public override void ExitAttack()
    {
        
    }

    /// <summary>
    /// Instantiates a projectile that handles it's own movement
    /// </summary>
    /// <param name="target"></param>
    /// <param name="projectilePrefab"></param>
    /// <param name="projectileStart"></param>
    private void Shoot(Transform target, GameObject projectilePrefab, Transform projectileStart)
    {
        GameObject projectileObj = Instantiate(projectilePrefab, projectileStart.position, this.transform.rotation);
    }
}
