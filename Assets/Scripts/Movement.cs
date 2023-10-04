using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] private float _backwardSpeed;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _brakeSpeed;
    [SerializeField] private float _maxVelocityMagnitude;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private Transform _transform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }
    
    public void Initialize()
    {
        
    }

    public void MoveForward()
    {
        AddMovement(_transform.forward, _forwardSpeed);
    }

    public void MoveBackward()
    {
        AddMovement(-_transform.forward, _backwardSpeed);
    }

    public void StrafeLeft()
    {
        AddMovement(-_transform.right, _sideSpeed);
    }

    public void StrafeRight()
    {
        AddMovement(_transform.right, _sideSpeed);
    }

    public void Brake()
    {
        Vector3 stopForce = -_rigidbody.velocity.normalized;
        AddMovement(stopForce, _brakeSpeed);
    }

    public void RotateLeft()
    {
        RotateByForce(-_rotationSpeed);
    }

    public void RotateRight()
    {
        RotateByForce(_rotationSpeed);
    }

    private void RotateByForce(float rotationSpeed)
    {
        _rigidbody.AddTorque(new Vector3(0f, rotationSpeed, 0f));
    }

    private void AddMovement(Vector3 direction, float _speed)
    {
        float changedMagnitude = (_rigidbody.velocity + direction).magnitude;

        if (changedMagnitude < _maxVelocityMagnitude || _rigidbody.velocity.magnitude > changedMagnitude)
            _rigidbody.AddForce(direction * Time.fixedDeltaTime * _speed);
    }    
}
