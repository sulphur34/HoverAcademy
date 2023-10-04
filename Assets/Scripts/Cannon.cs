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
    public void Initialize(Vehicle vehicle)
    {
        
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
