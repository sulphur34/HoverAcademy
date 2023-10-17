using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Cannon : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float _damage;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        EndFire();
        _damage = 5;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Health health))
            health.Damage(_damage);
    }

    public void Initialize(Vehicle vehicle)
    {
        _damage = vehicle.Damage;
    }

    public void StartFire()
    {
        _particleSystem.enableEmission = true;
    }

    public void EndFire()
    {
        _particleSystem.enableEmission = false;
    }
}
