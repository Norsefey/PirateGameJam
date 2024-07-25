using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] string[] tagsToDamage;
    [SerializeField] float _lifeTime;
    private float _damage;
    private float _speed;
    private Vector3 _direction;
    private bool _isInitialized = false;
    Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        Destroy(this.gameObject, _lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Move in y direction at x speed
    }

    private void FixedUpdate()
    {
        if (!_isInitialized) return;

        _rb.velocity = _direction.normalized * _speed * Time.deltaTime;
    }

    public void Initialize(float damage,float speed,  Vector3 direction)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction;
        _isInitialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Deal damage and destroy self
        if (tagsToDamage.Contains(other.gameObject.tag) && other.gameObject.TryGetComponent<Health>(out Health healthManager))
        {
            healthManager.RemoveHealth(_damage);
        }

        Destroy(this.gameObject);
    }
}
