using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public enum Type
    {
        Player,
        Enemy
    }

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

    [SerializeField]
    private Vector2 positionOffset = Vector2.zero;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    public UnityEvent OnDeath;
    public UnityEvent OnHealthChange;

    public bool IsDefending;

    private void Awake()
    {
        OnDeath = new UnityEvent();
        OnHealthChange = new UnityEvent();
        IsDefending = false;
        currentHealth = maxHealth;
        transform.position += new Vector3(positionOffset.x, positionOffset.y);
    }

    private void TakeDamage(int damage)
    {
        if (IsDefending)
        {
            damage /= 2;
            IsDefending = false;
        }
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

    public void DealDamage(Unit other, int? amount = null)
    {
        other.TakeDamage(amount ?? damage);
    }
}
