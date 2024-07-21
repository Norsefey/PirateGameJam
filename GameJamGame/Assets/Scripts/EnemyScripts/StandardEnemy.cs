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
        //Create the state machine
        _stateMachine = new StateMachine();

/*        //Create states
        var patrol = new PatrolState(this);
        var chase = new ChaseState(this);
        var attack = new AttackState(this);
        var flee = new FleeState(this);

        //Normal Transitions
        At(patrol, chase, HasTargetOutOfRange());
        At(patrol, attack, HasTargetInRange());

        At(chase, patrol, HasNoTarget());
        At(chase, attack, HasTargetInRange());

        At(attack, chase, HasTargetOutOfRange());
        At(attack, patrol, HasNoTarget());

        //Any Transitions
        _stateMachine.AddAnyTransition(flee, TargetsInRangeButLowHealth());
        //At(flee, patrol, () => sensing.EnemyInRange == false);

        //Set the default state
        _stateMachine.SetState(patrol);

        //At() is the shorthand of stateMachine.AddTransition
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        //Conditions
        Func<bool> HasTargetOutOfRange() => () => Target != null && Vector2.Distance(transform.position, Target.transform.position) > 5;
        Func<bool> HasNoTarget() => () => Target == null;
        Func<bool> HasTargetInRange() => () => Target != null && Vector2.Distance(transform.position, Target.transform.position) <= 5;
        Func<bool> TargetsInRangeButLowHealth() => () => Target != null && _health <= lowHealthThreshhold;*/
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
