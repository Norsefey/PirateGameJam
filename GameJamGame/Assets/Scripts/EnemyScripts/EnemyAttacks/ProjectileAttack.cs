using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileAttack : AttackBase
{
    [SerializeField] float _cooldownTime;
    [SerializeField] float _rotationSpeed;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _projectileStart;
    
    public override void DoAttack(Transform target)
    {
        base.DoAttack(target);

        bool isAimed = Aim(target, _rotationSpeed);

        if (!isAimed) return;
        Shoot(target, _projectilePrefab, _projectileStart);
        StartCoroutine(Cooldown(_cooldownTime));
    }

    public override void EnterAttack()
    {
        
    }

    public override void ExitAttack()
    {
        
    }

    /// <summary>
    /// Aim the projectile start point at the target
    /// </summary>
    /// <param name="target"></param>
    /// <param name="projectileStart"></param>
    /// <param name="rotationSpeed"></param>
    private bool Aim(Transform target, float rotationSpeed)
    {
        Vector3 direction = (target.position - this.gameObject.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        Debug.Log(Vector3.Dot(this.gameObject.transform.forward, direction));
        if (Vector3.Dot(this.gameObject.transform.forward, direction) > 0.995f)
        {
            return true;
        }
        else 
        {
            return false;
        } 
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
