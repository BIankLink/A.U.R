using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState newState)
    {

        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }
    public void HandleInput()
    {
        currentState.HandleInput();
    }
    public void Update(float deltaTime)
    {
        currentState?.Update(deltaTime);
    }
    public void PhysicsUpdate(float deltaTime)
    {
        currentState?.PhysicsUpdate(deltaTime);
    }
    public void OnAnimationEnterEvent()
    {
        currentState?.OnAnimationEnterEvent();
    }
    public void OnAnimationExitEvent()
    {
        currentState?.OnAnimationExitEvent();
    }
    public void OnAnimationTransitionEvent()
    {
        currentState?.OnAnimationTransitionEvent();
    }
    public void OnTriggerEnter(Collider collider)
    {
        currentState?.OnTriggerEnter(collider);
    }
    public void OnTriggerExit(Collider collider)
    {
            currentState?.OnTriggerExit(collider);
    }
}

