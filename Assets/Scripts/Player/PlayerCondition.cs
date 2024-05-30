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

    public void Heal(float amount)
    {
        _health.Add(amount);
    }

    //public void Buff(float amount)
    //{
        
    //}

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
