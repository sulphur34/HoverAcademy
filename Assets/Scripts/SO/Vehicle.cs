using UnityEngine;

[CreateAssetMenu(fileName = "new vehicle", menuName = "Vehicle", order = 53)]
public class Vehicle : ScriptableObject
{
    [SerializeField] private Hover _hoverPrefab;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _backwardSpeed;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _brakeSpeed;
    [SerializeField] private float _maxVelocityMagnitude;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _mass;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;

    public Hover Hover => _hoverPrefab;
    public float ForwardSpeed => _forwardSpeed;
    public float BackwardSpeed => _backwardSpeed;
    public float SideSpeed => _sideSpeed;
    public float BrakeSpeed => _brakeSpeed;
    public float MaxVelocityMagnitude => _maxVelocityMagnitude;
    public float RotationSpeed => _rotationSpeed;
    public float Mass => _mass;
    public float MaxHealth => _maxHealth;
    public float Damage => _damage;       

}
