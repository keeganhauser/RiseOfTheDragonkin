using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private string unitName;
    public string UnitName => unitName;

    [SerializeField]
    private int unitLevel;
    public int UnitLevel => unitLevel;

    [SerializeField]
    private int damage;
    public int Damage => damage;

    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    public UnityEvent OnDeath;
    public UnityEvent OnHealthChange;

    private void Awake()
    {
        OnDeath = new UnityEvent();
        OnHealthChange = new UnityEvent();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }

        OnHealthChange?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
