using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health), typeof(NavMesh))]
public abstract class StandardEnemy : MonoBehaviour
{
    [SerializeField] protected float _attackRange;
    [SerializeField] public float PursuitSpeed;
    [SerializeField] public float AvoidanceSpeed;
    protected bool _isDead = false;

    [SerializeField] protected GameObject target;
    public GameObject Target { get { return target; } set { target = value; } }
    

    protected Health _health;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public Rigidbody Rb;
    protected StateMachine _stateMachine;

    protected virtual void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        Rb = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _stateMachine = new StateMachine();

        //Event subscriptions
        _health.OnHealthDepleted += Die;
    }

    // Start is called before the first frame update
    void Start()
    {
        NavAgent.speed = PursuitSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();
    }

    protected virtual void Die()
    {
        _isDead = true;
    }

    protected virtual void OnDestroy()
    {
        //Unsubscribe any events
        _health.OnHealthDepleted -= Die;
    }
}
