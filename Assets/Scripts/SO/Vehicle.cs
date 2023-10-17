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

    [SerializeField] private float _engineMaxDistance;
    [SerializeField] private float _engineMaxForce;
    [SerializeField] private float _engineDamping;
    [SerializeField] private float _engineProgressivity;
    [SerializeField] private float _engineUpFactor;

    [SerializeField] private float _aimingAreaWidth;
    [SerializeField] private float _aimingDistance;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;

    public Hover Hover => _hoverPrefab;
    public float ForwardSpeed => _forwardSpeed;
    public float BackwardSpeed => _backwardSpeed;
    public float SideSpeed => _sideSpeed;
    public float BrakeSpeed => _brakeSpeed;
    public float MaxVelocityMagnitude => _maxVelocityMagnitude;
    public float RotationSpeed => _rotationSpeed;
    public float EngineMaxDistance => _engineMaxDistance;
    public float EngineMaxForce => _engineMaxForce;
    public float EngineDamping => _engineDamping;
    public float EngineProgressivity => _engineProgressivity;
    public float EngineUpFactor => _engineUpFactor;
    public float AimingAreaWidth => _aimingAreaWidth;
    public float AimingDistance => _aimingDistance;
    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    
    public GameObject BuildHover(Transform container)
    {
        Hover hover = Instantiate(_hoverPrefab.gameObject, container).GetComponent<Hover>();
        hover.Initialize(this);
        return hover.gameObject;
    }
}
