using UnityEngine;

public class Mover : MonoBehaviour
{
    
    [SerializeField] private float _backwardSpeed;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _brakeSpeed;
    [SerializeField] private float _maxVelocityMagnitude;

    private Rigidbody _rigidbody;
    private Transform _transform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }
    
    public void Initialize(Vehicle vehicle)
    {
        _backwardSpeed = vehicle.BackwardSpeed;
        _sideSpeed = vehicle.SideSpeed;
        _forwardSpeed = vehicle.ForwardSpeed;
        _brakeSpeed = vehicle.BrakeSpeed;
        _maxVelocityMagnitude = vehicle.MaxVelocityMagnitude;
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
   
    private void AddMovement(Vector3 direction, float _speed)
    {
        float changedMagnitude = (_rigidbody.velocity + direction).magnitude;

        if (changedMagnitude < _maxVelocityMagnitude || _rigidbody.velocity.magnitude > changedMagnitude)
            _rigidbody.AddForce(direction * Time.fixedDeltaTime * _speed);
    }    
}
