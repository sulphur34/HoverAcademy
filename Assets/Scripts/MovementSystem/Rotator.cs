using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void Initialize(Vehicle vehicle)
    {
        _rotationSpeed = vehicle.RotationSpeed;
    }

    public void RotateTowardsDirection(Vector3 targetDirection)
    {
        Vector3 rotationDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);
        Quaternion targetOrientation = Quaternion.LookRotation(rotationDirection);
        Quaternion rotationChange = targetOrientation * Quaternion.Inverse(_rigidbody.rotation);

        rotationChange.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 180f)
            angle -= 360f;

        if (Mathf.Abs(angle) > 1)
        {
            var targetAngularVelocity = axis * angle * Mathf.Deg2Rad / Time.deltaTime;
            targetAngularVelocity *= _rotationSpeed;
            _rigidbody.AddTorque(targetAngularVelocity - _rigidbody.angularVelocity, ForceMode.VelocityChange);
        }
        else
        {
            _rigidbody.AddTorque(-_rigidbody.angularVelocity, ForceMode.VelocityChange);
        }
    }

    public void RotateTowardsPosition(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        RotateTowardsDirection(direction.normalized);
    }
}
