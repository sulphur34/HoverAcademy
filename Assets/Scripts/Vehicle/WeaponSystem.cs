using UnityEngine;
using System.Linq;
using System;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] Cannon[] _cannons;

    private float _aimingAreaWidth = 4;
    private float _aimingDistance = 100;
    private Transform _transform;
    private Vector3 _leftAimBorder;
    private Vector3 _rightAimBorder;
    private Vector3 _originPosition;
    private Type _enemyType;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (TryGetTarget(out Vector3 targetPosition))
        {
            Activate(targetPosition);
        }
        else
        {
            Deactivate();
        }
    }

    public void Initialize(Vehicle vehicle, Type enemyType)
    {
        _enemyType = enemyType;
        _aimingAreaWidth = vehicle.AimingAreaWidth;
        _aimingDistance = vehicle.AimingDistance;

        foreach (Cannon cannon in _cannons)
        {
            cannon.Initialize(vehicle);
        }
    }
    public void Activate(Vector3 targetPosition)
    {
        foreach (Cannon cannon in _cannons)
        {
            cannon.transform.LookAt(targetPosition);
            cannon.StartFire();
        }
    }

    public void Deactivate()
    {
        foreach (Cannon cannon in _cannons)
        {
            cannon.EndFire();
        }
    }

    private bool TryGetTarget(out Vector3 targetPosition)
    {
        SetAimBorders();
        
        var enemiesPositions = FindObjectsOfType(_enemyType)
            .Select(hover => ((MonoBehaviour)hover).transform.position)
            .Where(hover => (hover - _originPosition).magnitude < _aimingDistance
            && IsHoverWithingAimZone(hover)
            && IsNoObstaclesInbetween(hover));

        targetPosition = enemiesPositions.FirstOrDefault();

        return enemiesPositions.Count() > 0;
    }

    private bool IsHoverWithingAimZone(Vector3 hoverPosition)
    {
        float wholeArea = GetTriangleArea(_originPosition, _rightAimBorder, _leftAimBorder);
        float rightArea = GetTriangleArea(_originPosition, _rightAimBorder, hoverPosition);
        float leftArea = GetTriangleArea(_originPosition, hoverPosition, _leftAimBorder);
        float frontArea = GetTriangleArea(hoverPosition, _rightAimBorder, _leftAimBorder);
        return Mathf.Approximately(rightArea + leftArea + frontArea, wholeArea);
    }

    private bool IsNoObstaclesInbetween(Vector3 hoverPosition)
    {
        
        if (Physics.Linecast(_transform.position, hoverPosition, out RaycastHit hitInfo))
        {
            if(hitInfo.collider.TryGetComponent(out Hover hover))
            {
                return true;
            }
        }

        return false;
    }

    private float GetTriangleArea(Vector3 pointA, Vector3 pointB, Vector3 pointC)
    {
        return 0.5f * Mathf.Abs((pointA.x * (pointB.z - pointC.z) + pointB.x * (pointC.z - pointA.z) + pointC.x * (pointA.z - pointB.z)));
    }

    private void SetAimBorders()
    {
        _originPosition = new Vector3(_transform.position.x, 0f, _transform.position.z);
        Vector3 forward = new Vector3(_transform.forward.x, 0f, _transform.forward.z);
        _leftAimBorder = Quaternion.Euler(0f, -_aimingAreaWidth, 0f) * forward * _aimingDistance + _originPosition;
        _rightAimBorder = Quaternion.Euler(0f, _aimingAreaWidth, 0f) * forward * _aimingDistance + _originPosition;
    }   
}
