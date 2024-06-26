using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatController : MonoBehaviour
{
    [field: SerializeField] public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; protected set; }

    [field: SerializeField] public int MaxMana { get; protected set; }
    public int CurrentMana { get; protected set; }
    [field: SerializeField] public int Speed { get; protected set; }

    [SerializeField] protected int damage;

    public abstract int Level { get; }
    public abstract string Name { get; }

    private bool isDefending;

    private string status;
    public string Status
    {
        get => status;
        set
        {
            status = value;
            GameEventsManager.Instance.CombatEvents.CombatStatusChange(status);
        }
    }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
        isDefending = false;
    }

    protected abstract void HandleTurn();

    protected virtual void TakeDamage(int amount)
    {
        if (isDefending)
        {
            amount /= 2;
            isDefending = false;
        }

        Status = $"{Name} took {amount} damage!";
        CurrentHealth -= amount;
        GameEventsManager.Instance.CombatEvents.HealthChange(this);

        if (CurrentHealth <= 0)
        {
            Debug.Log($"{Name} has died!");
            GameEventsManager.Instance.CombatEvents.LoseCombat(this);
        }
    }

    protected abstract void Attack();

    protected virtual void Defend()
    {
        Status = $"{Name} defended!";

        isDefending = true;
        EndTurn();
    }

    protected virtual void UseItem()
    {
        // TODO: Use item in combat
        // Use inventory to select an item to use

        // Invoke item's effects on this controller

        EndTurn();
    }

    protected virtual void Escape()
    {
        // TODO: Escape from combat
        Status = $"{Name} has escaped!";
        GameEventsManager.Instance.CombatEvents.Escape(this);
    }

    protected void EndTurn()
    {
        GameEventsManager.Instance.CombatEvents.EndTurn();
    }

    public void GainHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        GameEventsManager.Instance.CombatEvents.HealthChange(this);
    }

    public void GainMana(int amount)
    {
        CurrentMana = Mathf.Min(CurrentMana + amount, MaxMana);
    }
}
