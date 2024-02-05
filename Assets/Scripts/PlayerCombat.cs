using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerCombat : Combat
{
    private Player player;
    private EntityHealth health;

    private void Start()
    {
        player = Player.Instance;
        isDefending = false;
        inCombat = false;
    }

    public override void TakeDamage(int damage)
    {
        if (health == null) health = Player.Instance.GetComponent<EntityHealth>();
        if (isDefending)
        {
            damage /= 2;
            isDefending = false;
        }
        health.LoseHealth(damage);
        Debug.Log($"{player.Name} took {damage} damage!");

        if (health.Health <= 0)
        {
            Die();
            Debug.Log("Player has died!");
        }
    }

    protected override void Defend()
    {
        isDefending = true;
        EndTurn();
    }

    protected override void Die()
    {
        // TODO: Implement PlayerCombat.Die()
        throw new NotImplementedException();
    }

    public void UIAttack()
    {
        DealDamage(
            FindFirstObjectByType<CombatHandler>()
            .Enemy.GetComponent<EnemyCombat>());
    }

    public void UIDefend()
    {
        Defend();
    }
}
