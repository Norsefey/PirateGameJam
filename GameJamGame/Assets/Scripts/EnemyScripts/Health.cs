using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Properties
    [SerializeField] float _maxHealth;
    private float _currentHealth;

    // Events
    public event Action OnHealthIncreased;
    public event Action OnHealthDecreased;
    public event Action OnHealthDepleted;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {

    }

    public void AddHealth(float healthChange)
    {
        _currentHealth += healthChange;
        OnHealthIncreased?.Invoke();

        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
    }

    public void RemoveHealth(float healthChange)
    {
        _currentHealth -= healthChange;
        OnHealthDecreased?.Invoke();

        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnHealthDepleted?.Invoke();
        }
    }
}
