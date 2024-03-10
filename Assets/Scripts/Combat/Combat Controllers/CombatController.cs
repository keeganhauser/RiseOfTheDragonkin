using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatAction
{
    Attack,
    Defend,
    Item,
    Escape
}

public abstract class CombatController : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;

    [SerializeField] protected int maxMana;
    protected int currentMana;

    [SerializeField] protected int damage;

    public abstract int Level { get; }
    public abstract string Name { get; }

    bool isDefending;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        isDefending = false;
    }

    protected abstract void HandleTurn();

    protected void TakeDamage(CombatController controller, int amount)
    {
        if (controller != this) return;
        Debug.Log($"{Name} is trying to be attacked for {amount} damage");

        if (isDefending)
        {
            amount /= 2;
            isDefending = false;
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log($"{Name} has died!");
            GameEventsManager.Instance.CombatEvents.EntityDied(this);
        }
    }

    protected void Attack(CombatController opponentController)
    {
        GameEventsManager.Instance.CombatEvents.Attack(opponentController, damage);
        GameEventsManager.Instance.CombatEvents.EndTurn();
    }

    protected void Defend()
    {
        isDefending = true;
        GameEventsManager.Instance.CombatEvents.EndTurn();
    }

    // TODO: End turn after using an item somehow
    protected void GainHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    protected void GainMana(int amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
    }

}
