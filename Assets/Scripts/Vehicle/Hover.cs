using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(WeaponSystem))]
[RequireComponent(typeof(EngineSystem))]

public class Hover : MonoBehaviour
{
    [SerializeField] EngineSystem _engineSystem;
    [SerializeField] WeaponSystem _weaponSystem;
    [SerializeField] Rotator _rotation;
    [SerializeField] Mover _mover;
    [SerializeField] Health _health;

    private void Initialize(Vehicle vehicle)
    {
        _rotation.Initialize(vehicle);
        _mover.Initialize(vehicle);
        _health.Initialize(vehicle);
        _weaponSystem.Initialize(vehicle);
    }
}
