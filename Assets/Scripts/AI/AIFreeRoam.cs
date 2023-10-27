using UnityEngine;

[RequireComponent(typeof(AITargetChase))]
public class AIFreeRoam : AIState
{
    private AITargetChase _targetChase;

    private void OnEnable()
    {
        _targetChase = GetComponent<AITargetChase> ();
    }

    public override AIState Run()
    {
        if (IsPlayerInChaseRange())
        {
            return _targetChase;
        }
        else
        {
            if (IsDestinationReached())
            {
                SetRandomWaypointAsTarget();
                ResetRoute();
            }
            else if (IsRootPointReached())
            {
                if (TrySwitchRoutePoint() == false)
                    ResetRoute();
            }

            if (IsPlayerInAttackRange() == false)
            {
                MoveToTarget();
            }

            RotateToTarget();
        }

        DrawPath();

        return this;
    }


}
