using UnityEngine;

public class AITargetChase : AIState
{
    [SerializeField] private AIFreeRoam _freeRoam;

    public override AIState Run()
    {
        if (IsPlayerInChaseRange() == false)
        {
            return _freeRoam;
        }
        else
        {
            SetPlayerPositionAsTarget();
            ResetRoute();

            if (IsPlayerInAttackRange())
            {
                RotateToTarget();
            }
            else
            {
                if (IsRootPointReached())
                {
                    if (TrySwitchRoutePoint() == false)
                        ResetRoute();
                }

                MoveToTarget();
                RotateToTarget();
            }
        }

        DrawPath();

        return this;
    }   
}
