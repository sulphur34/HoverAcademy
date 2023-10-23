using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(WeaponSystem))]
[RequireComponent(typeof(EngineSystem))]

public class Hover : MonoBehaviour
{
    [SerializeField] private ParticleSystem _burst;

    private EngineSystem _engineSystem;
    private WeaponSystem _weaponSystem;
    private Rotator _rotation;
    private Mover _mover;
    private Health _health;

    public Health Health => _health;

    private void Awake()
    {
        _engineSystem = GetComponent<EngineSystem>();
        _weaponSystem = GetComponent<WeaponSystem>();
        _rotation = GetComponent<Rotator>();
        _mover = GetComponent<Mover>();
        _health = GetComponent<Health>();
        _health.Death += Disable;
    }

    public void Initialize(Vehicle vehicle, Type enemyType)
    {
        _rotation.Initialize(vehicle);
        _mover.Initialize(vehicle);
        _health.Initialize(vehicle);
        _weaponSystem.Initialize(vehicle, enemyType);
        _engineSystem.Initialize(vehicle);
    }

    public void Disable()
    {
        StartCoroutine(Crash());
    }

    public IEnumerator Crash()
    {
        _engineSystem.Crash();
        _weaponSystem.Deactivate();
        _weaponSystem.enabled = false;
        float crashForce = 20;
        float crashTorque = 100;
        float crashDelay = 2;
        var rigidbody = GetComponent<Rigidbody>();        
        rigidbody.AddForce(transform.up * crashForce);
        rigidbody.AddTorque(transform.right * crashTorque);
        Instantiate(_burst, transform).Play();
        yield return new WaitForSeconds(crashDelay);
        Instantiate(_burst, transform).Play();
        yield return new WaitForSeconds(_burst.main.duration);
        gameObject.SetActive(false);
    }

    public void Restore()
    {
        _engineSystem.Restore();
        _weaponSystem.enabled=true;
    }
}
