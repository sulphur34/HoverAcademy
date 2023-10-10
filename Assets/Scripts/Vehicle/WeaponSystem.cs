using UnityEngine;
using System.Linq;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] Cannon[] _cannons;

    private float _aimingAreaWidth = 4;
    private float _aimingDistance = 100;
    private Transform _transform;
    private Vector3 _leftAimBorder;
    private Vector3 _rightAimBorder;
    private Vector3 _originPosition;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (IsTargetFound())
            Activate();
        else
            Deactivate();
    }

    public void Initialize(Vehicle vehicle)
    {
        foreach (Cannon cannon in _cannons)
        {
            cannon.Initialize(vehicle);
        }
    }

    private bool IsTargetFound()
    {
        SetAimBorders();
        
        var enemiesPositions = FindObjectsOfType<Hover>()
            .Select(hover => hover.transform.position)
            .Where(hover => (hover - _originPosition).magnitude < _aimingDistance
            && IsHoverWithingAimZone(hover)
            && IsNoObstaclesInbetween(hover));

        return enemiesPositions.Count() > 0;
    }

    public bool IsHoverWithingAimZone(Vector3 hoverPosition)
    {
        float wholeArea = GetTriangleArea(_originPosition, _rightAimBorder, _leftAimBorder);
        float rightArea = GetTriangleArea(_originPosition, _rightAimBorder, hoverPosition);
        float leftArea = GetTriangleArea(_originPosition, hoverPosition, _leftAimBorder);
        float frontArea = GetTriangleArea(hoverPosition, _rightAimBorder, _leftAimBorder);
        return Mathf.Approximately(rightArea + leftArea + frontArea, wholeArea);
    }

    public bool IsNoObstaclesInbetween(Vector3 hoverPosition)
    {
        
        if (Physics.Linecast(_transform.position, hoverPosition, out RaycastHit hitInfo))
        {
            if(hitInfo.collider.TryGetComponent(out Hover hover))
            {
                Debug.DrawLine(_transform.position, hoverPosition, Color.yellow);
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
        Debug.DrawLine(_rightAimBorder, _leftAimBorder, Color.green);
        Debug.DrawLine(transform.position, _leftAimBorder, Color.red);
        Debug.DrawLine(transform.position, _rightAimBorder, Color.red);
    }

    //private void SetRaycastDirections()
    //{
    //    Vector3 forward = _transform.forward;
    //    Vector3 origin = _transform.position;
    //    int index = 0;

    //    for (float i = -2; i <= 2; i++)
    //    {
    //        for (float j = -2; j <= 2; j++)
    //        {
    //            Vector3 direction = Quaternion.Euler(i * _aimingAreaWidth, j * _aimingAreaWidth, 0) * forward;
    //            _rays[index++] = new Ray(origin, direction);
    //        }
    //    }
    //}

    //private void GenerateRaycasts()
    //{
    //    for (int i = 0; i < _raycastNumber; i++)
    //    {
    //        Debug.DrawRay(_rays[i].origin, _rays[i].direction * 100, Color.red);
    //        if (Physics.Raycast(_rays[i], out RaycastHit hitInfo))
    //            Debug.DrawRay(transform.position, hitInfo.point, Color.yellow);
    //    }
    //}

    public void Activate()
    {
        foreach (Cannon cannon in _cannons)
        {
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
}
