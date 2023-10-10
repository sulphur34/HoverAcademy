using System;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    [SerializeField] private AIState _currentState;

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        AIState nextState = _currentState?.Run();

        if(nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(AIState nextState)
    {
        _currentState = nextState;
    }
}
