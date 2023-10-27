using UnityEngine;

[RequireComponent(typeof(AIFreeRoam))]
public class AITargetChase : AIState
{
    private AIFreeRoam _freeRoam;

    private void Start()
    {
        _freeRoam = GetComponent<AIFreeRoam>();
    }

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

        return this;
    }   
}
