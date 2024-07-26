using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health), typeof(NavMeshAgent), typeof(Rigidbody))]
public abstract class EnemyBase : MonoBehaviour
{
    
    [SerializeField] protected float _attackRange;
    [SerializeField] public float Speed;

    [Header("Randomization")]
    [SerializeField] protected float _speedChange;
    [SerializeField] protected float _minSpeed;
    [SerializeField] protected float _scaleChange;

    [Header("")]
    [SerializeField] protected GameObject target;
    public GameObject Target { get { return target; } set { target = value; } }

    protected bool _isDead;


    protected Health _health;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public Rigidbody Rb;
    protected StateMachine _stateMachine;

    protected virtual void Awake()
    {
        // Components
        NavAgent = GetComponent<NavMeshAgent>();
        Rb = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        
        //State Machine
        _stateMachine = new StateMachine();

        //Event Subscriptions
        _health.OnHealthDepleted += Die;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Randomize();
        
        _isDead = false;
        NavAgent.speed = Speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _stateMachine.Tick();
    }

    protected virtual void OnDestroy()
    {
        //Unsubscribe any events
        _health.OnHealthDepleted -= Die;
    }

    protected virtual void Die()
    {
        _isDead = true;
        Destroy(this.gameObject);
    }

    protected virtual void Randomize()
    {
        // Randomize Scale
        float randScale = UnityEngine.Random.Range(-_scaleChange, _scaleChange);
        Vector3 scaleOffset = new Vector3(randScale, randScale, randScale);
        Vector3 newScale = (gameObject.transform.localScale) + scaleOffset;
        if (newScale.x > 0 && newScale.y > 0 && newScale.z > 0) gameObject.transform.localScale += scaleOffset;  //Negative scales = bad

        // Randomize speed
        float randSpeed = UnityEngine.Random.Range(-_speedChange, _speedChange);
        float newSpeed = Speed + randSpeed;
        if(newSpeed >= _minSpeed) Speed = newSpeed;
        else Speed = _minSpeed;
    }

    #region State Machine Functions
    // Helper Functions
    // At() is the shorthand of stateMachine.AddTransition
    protected void At(IState to, IState from, Func<bool>[] conditions) => _stateMachine.AddTransition(to, from, conditions);
    protected Func<bool> Not(Func<bool> condition)
    {
        return () => !condition();
    }

    protected Func<bool> And(Func<bool> conditionA, Func<bool> conditionB)
    {
        return () => conditionA() && conditionB();
    }

    protected Func<bool> Or(Func<bool> conditionA, Func<bool> conditionB)
    {
        return () => conditionA() || conditionB();
    }


    // Basic State Transition Conditions
    protected Func<bool> HasTarget => () => Target != null;
    protected Func<bool> TargetInRange => () => Vector3.Distance(transform.position, Target.transform.position) <= _attackRange;
    protected Func<bool> IsDead => () => _isDead == true;
    #endregion
}
