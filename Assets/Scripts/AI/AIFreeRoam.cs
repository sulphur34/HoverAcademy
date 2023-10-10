using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rotator))]
public class AIMovement : AIState
{
    [SerializeField] private Waypoint[] _waypoints;

    private Waypoint _currentWaypoint;
    private System.Random _random;
    private Vector3 _currentDestination;    
    private Coroutine _rootTrack;
    private NavMeshPath _path;
    private Queue<Vector3> _route;
    private event UnityAction _onWaypointReached;
    private LayerMask _collisionLayerMask;
    private float _positionTolerance;

    private void Start()
    {
        _random = new System.Random();
        _path = new NavMeshPath();
        _onWaypointReached += ResetRoute;
        _collisionLayerMask = 8;
        _positionTolerance = 10;
        GetRandomWaypoint();
        ResetRoute();
        
    }

    private void FixedUpdate()
    {
        DrawPath(_path.corners);
        MoveToTarget();
        RotateToTarget();
    }

    

    private void ResetRoute()
    {
        GetPathToPoint(_currentWaypoint.transform.position);

        if (_route.Count > 0)
        {
            _currentDestination = _route.Dequeue();
            if (_rootTrack != null)
                StopCoroutine(_rootTrack);

            _rootTrack = StartCoroutine(SwitchDestination());
        }
        else
        {
            GetRandomWaypoint();
            ResetRoute();
        }
    }

    private IEnumerator SwitchDestination()
    {
        while (_route.Count > 0)
        {
            if (Vector3.Distance(_currentDestination, _transform.position) > _positionTolerance)
            {
                Debug.DrawLine(_transform.position, _currentDestination, Color.cyan);
                yield return new WaitForFixedUpdate();
            }
            else
            {
                _currentDestination = _route.Dequeue();
            }
        }

        GetRandomWaypoint();
        _onWaypointReached.Invoke();
    }

    private void GetRandomWaypoint()
    {
        int index = _random.Next(_waypoints.Length);
        _currentWaypoint = _waypoints[index];
    }    

    private void DrawPath(Vector3[] path)
    {
        for (int i = 0; i < path.Length - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], Color.blue);
        }
    }

    public override AIState RunCurrentState()
    {
        throw new System.NotImplementedException();
    }
}
