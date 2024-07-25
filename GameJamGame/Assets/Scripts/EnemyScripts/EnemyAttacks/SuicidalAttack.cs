using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SuicidalAttack : AttackBase
{
    [SerializeField] float _cooldownTime;
    [SerializeField] float _ramForce;
    [SerializeField] float _recoveryThreshhold;
    [SerializeField] Rigidbody _rigidbody;
    private bool _isRecovered = true;
    public override void DoAttack(Transform target)
    {
        base.DoAttack(target);

        RamIntoTarget(target, _rigidbody, _ramForce);

        StartCoroutine(Recover(_rigidbody));

        StartCoroutine(Cooldown(_cooldownTime));
    }

    private void RamIntoTarget(Transform target, Rigidbody rb, float ramForce)
    {
        // Calculate Direction
        Vector3 direction = (target.position - rb.position).normalized;

        rb.AddForce(direction * ramForce, ForceMode.Impulse);
    }

    private IEnumerator Recover(Rigidbody rb)
    {
        _isRecovered = false;
        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => rb.velocity.magnitude <= _recoveryThreshhold);
        _isRecovered = true;
    }

    public void EnableRigidbody(Rigidbody rb)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void DisableRigidbody(Rigidbody rb)
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public bool IsRecovered()
    {
        return _isRecovered;
    }

    public override void EnterAttack()
    {
        EnableRigidbody(_rigidbody);
    }

    public override void ExitAttack()
    {
        DisableRigidbody(_rigidbody);
    }

    public override bool CanAttack()
    {
        return base.CanAttack() && IsRecovered();
    }
}
