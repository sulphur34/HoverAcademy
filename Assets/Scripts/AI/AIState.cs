using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rotator))]
public abstract class AIState : MonoBehaviour
{
    private float _positionTolerance;
    private Waypoint[] _waypoints;
    private Player _player;
    private Transform _transform;
    private Mover _movement;
    private Rotator _rotation;
    private NavMeshPath _path;
    private Queue<Vector3> _routePoints;
    private Vector3 _currentTarget;
    private Vector3 _currentRoutePoint;
    private float _chaseDistance;
    private LayerMask _collisionLayerMask;
    private System.Random _random;
    private float _attackDistance;

    private void Awake()
    {
        _random = new System.Random();
        _path = new NavMeshPath();
        _transform = transform;
        _movement = GetComponent<Mover>();
        _rotation = GetComponent<Rotator>();
        _collisionLayerMask = 8;
        _positionTolerance = 8;
        _chaseDistance = 200;
        _attackDistance = 50;
        _currentTarget = _transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _collisionLayerMask)
            ResetRoute();
    }

    public void Initialize(Waypoint[] waypoints, Player player)
    {
        _waypoints = waypoints;
        _player = player;
    }

    public abstract AIState Run();

    protected bool TrySwitchRoutePoint()
    {
        if (_routePoints.Count > 0)
        {
            _currentRoutePoint = _routePoints.Dequeue();
            return true;
        }

        return false;
    }

    protected void ResetRoute()
    {
        GetPathToPoint(_currentTarget);
        TrySwitchRoutePoint();
    }

    protected bool IsPlayerInChaseRange()
    {
        if (_player != null
            && Vector3.Distance(transform.position, _player.transform.position) < _chaseDistance)
            return true;

        return false;
    }

    protected bool IsDestinationReached()
    {
        return IsPointReached(_currentTarget);
    }

    protected bool IsRootPointReached()
    {
        return IsPointReached(_currentRoutePoint);
    }

    protected bool IsPlayerInAttackRange()
    {
        if (_player != null
            && Vector3.Distance(transform.position, _player.transform.position) < _attackDistance)
            return true;

        return false;
    }

    protected void RotateToTarget()
    {
        _rotation.RotateTowardsPosition(_currentTarget);
    }

    protected void MoveToTarget()
    {
        GetMovementToTarget(_transform.forward, _movement.MoveForward, _movement.MoveBackward).Invoke();
        GetMovementToTarget(_transform.right, _movement.StrafeRight, _movement.StrafeLeft).Invoke();
    }

    protected void SetRandomWaypointAsTarget()
    {
        int index = _random.Next(_waypoints.Length);
        _currentTarget = _waypoints[index].transform.position;
    }

    protected void SetPlayerPositionAsTarget()
    {
        _currentTarget = _player.transform.position;
    }

    protected void DrawPath()
    {
        Debug.DrawLine(transform.position, _currentRoutePoint);

        for (int i = 0; i < _path.corners.Length - 1; i++)
        {
            Debug.DrawLine(_path.corners[i], _path.corners[i + 1], Color.blue);
        }
    }

    private bool IsPointReached(Vector3 point)
    {
        return (Vector3.Distance(_transform.position, point) < _positionTolerance);
    }

    private void GetPathToPoint(Vector3 point)
    {
        Vector3 position = _transform.position;
        _routePoints = new Queue<Vector3>();
        bool IsInsideWalkableArea = NavMesh.SamplePosition(position, out NavMeshHit hit, _positionTolerance, NavMesh.AllAreas);

        if (IsInsideWalkableArea)
        {
            bool test = NavMesh.CalculatePath(hit.position, point, NavMesh.AllAreas, _path);

            for (int i = 0; i < _path.corners.Length; i++)
            {
                _routePoints.Enqueue(new Vector3(_path.corners[i].x, position.y, _path.corners[i].z));
            }
        }
    }

    private System.Action GetMovementToTarget(Vector3 direction, System.Action directMove, System.Action reverseMove)
    {
        Vector3 originPosition = _transform.position;
        Vector3 directionOffset = originPosition + direction;

        if (Vector3.Distance(_currentRoutePoint, originPosition) > Vector3.Distance(_currentRoutePoint, directionOffset))
            return directMove;
        else
            return reverseMove;
    }
}
