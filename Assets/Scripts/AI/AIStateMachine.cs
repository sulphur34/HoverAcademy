using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AIFreeRoam))]
public class AIStateMachine : MonoBehaviour
{
    private AIState _currentState;
    private Coroutine _coroutine;

    private void Awake()
    {
        _currentState = GetComponent<AIFreeRoam>();
    }

    public void SetDefaultState(AIState defaultState)
    {
        _currentState = defaultState;
    }

    public void Enable()
    {
        _coroutine = StartCoroutine(Run());
    }

    public void Disable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Run()
    {
        bool isContinue = true;

        while (isContinue)
        {
            AIState nextState = _currentState?.Run();

            if (nextState != null)
                SwitchToNextState(nextState);

            yield return new WaitForFixedUpdate();
        }
    }

    private void SwitchToNextState(AIState nextState)
    {
        _currentState = nextState;
    }
}
