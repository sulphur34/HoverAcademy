using UnityEngine;

public class AIFreeRoam : AIState
{
    [SerializeField] private AITargetChase _targetChase;

    private void Start()
    {
        SetRandomWaypointAsTarget();
        ResetRoute();
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
