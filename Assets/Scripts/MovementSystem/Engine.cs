using UnityEngine;

public class Engine : MonoBehaviour
{
    
    [SerializeField] private float _maxDistance = 2f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _damping;
    [SerializeField] private float _progressivity = 1f;
    [SerializeField] private float _upFactor;

    private Transform _transform;
    private Vector3 _engineWorldSpeed;
    private Vector3 _engineWorldOldPosition;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.forward;
        Vector3 forceDirection = Vector3.up;

        if (Physics.Raycast(_transform.position, forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Lift(forward, hitInfo.distance, out forceDirection);
        }

        Damping(forceDirection);
    }
    public void Initialize(Vehicle vehicle)
    {
        _maxDistance = vehicle.EngineMaxDistance;
        _maxForce = vehicle.EngineMaxForce;
        _damping = vehicle.EngineDamping;
        _progressivity = vehicle.EngineProgressivity;
        _upFactor = vehicle.EngineUpFactor;
    }

    private void Lift(Vector3 forward, float distance, out Vector3 forceDirection)
    {
        var forceFactor = Mathf.Pow(Mathf.Clamp01(1f - distance / _maxDistance), _progressivity);
        float force = _maxForce * forceFactor;
        forceDirection = Vector3.Slerp(-forward, Vector3.up,_upFactor);

        _rigidbody.AddForceAtPosition(forceDirection * force, _transform.position
            , ForceMode.Force);
    }

    private void Damping(Vector3 forceDirection)
    {
        _engineWorldSpeed =( _transform.position - _engineWorldOldPosition) * Time.fixedDeltaTime;
        float dotResult = Mathf.Clamp01(Vector3.Dot(forceDirection.normalized, _engineWorldSpeed.normalized));
        _rigidbody.AddForceAtPosition(-forceDirection * _engineWorldSpeed.magnitude * dotResult * _damping, _transform.position, ForceMode.Force);

        _engineWorldOldPosition = _transform.position;
    }    
}
