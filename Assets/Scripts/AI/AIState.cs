using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIState : MonoBehaviour
{
    private Transform _transform;
    private Mover _movement;
    private Rotator _rotation;
    private Vector3 _currentDestination;
    private Vector3 _currentRoutePoint;
    private NavMeshPath _path;
    private Queue<Vector3> _routePoints;
    private float _positionTolerance;
    private Player _player;
    private float _chaseDistance;
    protected AIState _nextState;
    private LayerMask _collisionLayerMask;

    private void Start()
    {
        _transform = transform;
        _movement = GetComponent<Mover>();
        _rotation = GetComponent<Rotator>();
        _player = FindObjectOfType<Player>();
        _collisionLayerMask = 8;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _collisionLayerMask)
        {
            ResetRoute();
        }
    }
    public abstract AIState RunCurrentState();

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
        GetPathToPoint(_currentDestination);
        TrySwitchRoutePoint();
    }

    protected bool IsPlayerInRange()
    {
        if (_player != null
            && Vector3.Distance(transform.position, _player.transform.position) < _chaseDistance)
            return true;

        return false;
    }

    protected void GetPathToPoint(Vector3 point)
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

    protected void RotateToTarget()
    {
        _rotation.RotateTowardsPosition(_currentRoutePoint);
    }

    protected void MoveToTarget()
    {
        GetMovementToTarget(_transform.forward, _movement.MoveForward, _movement.MoveBackward).Invoke();
        GetMovementToTarget(_transform.right, _movement.StrafeRight, _movement.StrafeLeft).Invoke();
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
