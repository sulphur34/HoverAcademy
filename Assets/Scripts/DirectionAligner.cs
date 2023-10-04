using UnityEngine;

public class DirectionAligner : MonoBehaviour
{
    [SerializeField] private Transform _alignPosition;
    [SerializeField] private Rigidbody _rotatedObject;
    [SerializeField] private float _rotationSpeed;

    private void FixedUpdate()
    {
        RotateTowardsDirection();
    }

    private void RotateTowardsDirection()
    {
        Vector3 targetDirection = new Vector3(_alignPosition.forward.x, 0f, _alignPosition.forward.z);
        Quaternion targetOrientation = Quaternion.LookRotation(targetDirection);
        Quaternion rotationChange = targetOrientation * Quaternion.Inverse(_rotatedObject.rotation);

        rotationChange.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 180f)
            angle -= 360f;

        if (Mathf.Abs(angle) > 1)
        {
            var targetAngularVelocity = axis * angle * Mathf.Deg2Rad / Time.deltaTime;
            targetAngularVelocity *= _rotationSpeed;
            _rotatedObject.AddTorque(targetAngularVelocity - _rotatedObject.angularVelocity, ForceMode.VelocityChange);
        }
        else
        {
            _rotatedObject.AddTorque(-_rotatedObject.angularVelocity, ForceMode.VelocityChange);
        }
    }
}
