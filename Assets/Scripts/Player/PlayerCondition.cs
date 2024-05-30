using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition _uiCondition;

    public Condition _health { get { return _uiCondition._health; } }

    public event Action onTakeDamge;

    private void Update()
    {
        Heal(_health._passiveValue * Time.deltaTime);
    }

    public void Heal(float amount)
    {
        _health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어 사망");
    }

    public void TakePhysicalDamage(int damagedAmount)
    {
        _health.Sub(damagedAmount);
        onTakeDamge?.Invoke();
    }
}
