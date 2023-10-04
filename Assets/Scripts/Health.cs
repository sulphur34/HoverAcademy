using UnityEngine;

public class Health : MonoBehaviour
{
    private float _maxHealth;
    private float _minHealth = 0;
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void Restore(float restoreValue)
    {
        _currentHealth += restoreValue;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
    }

    public void Damage(float damageValue)
    {
        _currentHealth -= damageValue;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);

        if (_currentHealth == _minHealth)
            Die();
    }

    public void Die()
    {

    }
}
