using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SuicidalAttack : AttackBase
{
    [SerializeField] float _damage;
    [SerializeField] float _cooldownTime;
    [SerializeField] float _ramForce;
    [SerializeField] float _recoveryThreshhold;
    [SerializeField] float _damageDuration; //Amount of time after an attack this object will deal damage on contact
    [SerializeField] float _rotationSpeed;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Health _health;

    private bool _isDealingDamage = false;
    private bool _isRecovered = true;
    public override void DoAttack(Transform target)
    {
        base.DoAttack(target);

        bool isAimed = Aim(target, _rotationSpeed);

        if (!isAimed) return;
        RamIntoTarget(target, _rigidbody, _ramForce);
        StartCoroutine(IsDamaging(_damageDuration));
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

    private IEnumerator IsDamaging(float damageDuration)
    {
        _isDealingDamage = true;
        yield return new WaitForSeconds(damageDuration);
        _isDealingDamage = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && _isDealingDamage)
        {
            if (FuelManager.instance) FuelManager.instance.UseFuel(_damage);
            _health.RemoveHealth(1000);
        }
    }
}
