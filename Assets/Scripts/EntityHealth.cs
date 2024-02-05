using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public int Health => health;

    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth => maxHealth;

    public void LoseHealth(int amount)
    {
        health -= amount;
        Debug.Log($"health is now {health} ({Health})");
    }

    public void GainHealth(int amount)
    {
        health = (int)Mathf.Clamp(health + amount, 0f, maxHealth);
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
    }
}
