using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerCombat : Combat
{
    private Player player;

    private void Start()
    {
        player = Player.Instance;
        isDefending = false;
        inCombat = false;
    }

    public override void TakeDamage(int damage)
    {
        if (isDefending)
        {
            damage /= 2;
            isDefending = false;
        }
        player.Health -= damage;
        Debug.Log($"{player.Name} took {damage} damage!");

        if (player.Health <= 0)
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
        foreach (EnemyCombat enemy in FindObjectOfType<CombatHandler>().Enemies)
        {
            DealDamage(enemy);
        }
    }

    public void UIDefend()
    {
        Defend();
    }
}
