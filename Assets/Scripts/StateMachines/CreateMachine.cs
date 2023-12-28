using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private IState _currentState;

    public void ChangeState(IState state)
    {

            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();

    }

    public void HandleInput()
    {
        _currentState?.HandleInput();
    }

    public void Update()
    {
        _currentState.Update();
    }

    public void PhysicsUpdate()
    {
        _currentState?.PhysicsUpdate();
    }
}
