using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 200;
    [SerializeField] private float _currentHealth = 200;

    private float _minHealth = 0;
    private bool _isAlive = true;

    public event UnityAction HealthChanged;
    public event UnityAction Death;
    public event UnityAction HealthReset;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void OnEnable()
    {
        HealthChanged?.Invoke();
    }

    public void Initialize(Vehicle vehicle)
    {
        _maxHealth = vehicle.MaxHealth;
        _currentHealth = _maxHealth;
    }

    public void Restore(float restoreValue)
    {
        _currentHealth += restoreValue;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        HealthChanged?.Invoke();
    }

    public void Damage(float damageValue)
    {
        Restore(-damageValue);

        if (_currentHealth == _minHealth && _isAlive)
            Die();
    }

    public void Reset()
    {
        Restore(_maxHealth);
        _isAlive = true;
        HealthReset?.Invoke();
    }

    public void Die()
    {
        _isAlive = false;
        Death?.Invoke();
    }
}
