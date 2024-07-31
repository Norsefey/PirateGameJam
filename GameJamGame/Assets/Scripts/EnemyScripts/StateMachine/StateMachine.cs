using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Selectable;

//https://www.youtube.com/watch?v=V75hgcsCGOM 
public class StateMachine
{

    private IState _currentState;

    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    private List<Transition> _anyTransitions = new List<Transition>();
    
    private static List<Transition> EmptyTransitions = new List<Transition>(capacity: 0);


    public void Tick()
    {
        //Look for a transition, if found, switch to the destination state of that transition
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);

        //If no transitions, continue to Tick the current state
        _currentState?.Tick();
    }

    public void SetState(IState state)
    {
        //if the passed state is the current state exit because we don't want to repeatedly enter the same state
        if (state == _currentState)
            return;

        _currentState?.OnExit(); //clean up
        _currentState = state; //set current state to passed state so when we Tick the proper state Ticks

        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
            _currentTransitions = EmptyTransitions;

        _currentState.OnEnter();
    }


    public void AddTransition(IState from, IState to, Func<bool>[] predicates)
    {
        //try to get the list of transitions for the from state
        if(_transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicates));
    }

    public void AddAnyTransition(IState state, Func<bool>[] predicates)
    {
        _anyTransitions.Add(new Transition(state, predicates));
    }

    private class Transition
    {
        public Func<bool>[] Conditions { get; }
        public IState To { get; }

        public bool ConditionsMet()
        {
            foreach (var condition in Conditions)
            {
                if (!condition()) return false;
            }
            return true;
        }

        public Transition(IState to, Func<bool>[] conditions)
        {
            To = to;
            Conditions = conditions;
        }
    }

    private Transition GetTransition()
    {
        //_anyTransitions are transitions coming from any state, because they don't have a from state
        foreach (var transition in _anyTransitions)
        {
            if (transition.ConditionsMet())
            {
                return transition;
            }          
        }


        //current transitions are transitions for the current state
        foreach (var transition in _currentTransitions)
        {
            if (transition.ConditionsMet())
            {
                return transition;
            }
        }
      
        return null;
    }

}
