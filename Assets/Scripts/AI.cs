using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Waypoint[] _waypoints;

    private NavMeshPath _path;
    private System.Random _random;
    private Waypoint _currentWaypoint;
    private Transform _transform;

    private void Awake()
    {
        _random = new System.Random();
        _transform = transform;
        _path = new NavMeshPath();
        GetRandomWaypoint();
    }

    private void Update()
    {
        GetPathToWaypoint();
    }

    private void GetRandomWaypoint()
    {
        //int index = _random.Next(_waypoints.Length);
        _currentWaypoint = _waypoints[5];
    }

    private void GetPathToWaypoint()
    {
        bool test = NavMesh.CalculatePath(_transform.position, _currentWaypoint.transform.position,NavMesh.AllAreas, _path);
        DrawPath();
    }

    private void DrawPath()
    {
        Vector3 currentPosition = _transform.position;

        for (int i = 0; i < _path.corners.Length; i++)
        {
            Debug.DrawLine(currentPosition, _path.corners[i]);
            currentPosition = _path.corners[i];
        }
    }
}
