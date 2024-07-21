using System;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{
    public GameObject Target { get; set; }
    private StateMachine _stateMachine;

    private float _health;
    private float lowHealthThreshhold;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();
    }
}
