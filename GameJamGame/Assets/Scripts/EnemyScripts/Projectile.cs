using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] string[] tagsToDamage;
    [SerializeField] float _lifeTime;
    [SerializeField] float _damage;
    [SerializeField] float _speed;
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
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.forward.normalized * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Deal damage and destroy self
        if (tagsToDamage.Contains(other.gameObject.tag))
        {
            if (FuelManager.instance)
            {
                FuelManager.instance.UseFuel(_damage);
            }
        }

        Destroy(this.gameObject);
    }
}
