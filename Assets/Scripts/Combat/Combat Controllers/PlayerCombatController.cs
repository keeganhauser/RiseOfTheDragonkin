using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : CombatController
{    
    public override int Level => 1;
    public override string Name => "Player Name";

    public void Initialize(int health, int mana, int damage, int speed)
    {
        MaxHealth = CurrentHealth = health;
        MaxMana = CurrentMana = mana;
        this.damage = damage;
        Speed = speed;
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onPlayerTurn += HandleTurn;
        GameEventsManager.Instance.EnemyEvents.onEnemyAttack += TakeDamage;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onPlayerTurn -= HandleTurn;
        GameEventsManager.Instance.EnemyEvents.onEnemyAttack -= TakeDamage;
    }

    protected override void HandleTurn()
    {
        Debug.Log("Handling player turn");
    }

    protected override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        // TODO: Handle the player losing better than this
        if (CurrentHealth <= 0)
            CurrentHealth = MaxHealth;
    }

    protected override void Attack()
    {
        GameEventsManager.Instance.PlayerEvents.PlayerDecideAttack();
        GameEventsManager.Instance.PlayerEvents.PlayerAttack(damage);
        GameEventsManager.Instance.CombatEvents.EndTurn();
    }

    public void ButtonAttack()
    {
        Attack();
    }

    public void ButtonDefend()
    {
        Defend();
    }

    public void ButtonItem()
    {
        // Show the list of items to use
        GameEventsManager.Instance.InventoryEvents.InventoryListToggle();

        // Start listening to use event
        GameEventsManager.Instance.InventoryEvents.onInventoryRemoveItem += InventoryListUseItem;
        Debug.Log("Choosing an item...");
    }

    private void InventoryListUseItem(Item item, int amount)
    {
        // Stop listening to event
        GameEventsManager.Instance.InventoryEvents.onInventoryRemoveItem -= InventoryListUseItem;


        // Toggle inventory list off
        GameEventsManager.Instance.InventoryEvents.InventoryListToggle();

        // Update status
        Status = $"You used {amount} {item.ItemData.itemName}!";
        Debug.Log("PCC used item");
        // End turn
        GameEventsManager.Instance.CombatEvents.EndTurn();
    }

    public void ButtonEscape()
    {
        Escape();
    }
}
