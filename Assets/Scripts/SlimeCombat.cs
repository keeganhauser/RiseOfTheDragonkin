using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCombat : EnemyCombat
{
    private Slime slime;
    private EntityHealth health;

    private void Start()
    {
        slime = GetComponent<Slime>();
        health = GetComponent<EntityHealth>();
    }

    public override void TakeDamage(int damage)
    {
        if (isDefending)
        {
            damage /= 2;
            isDefending = false;
        }
        health.LoseHealth(damage);
        Debug.Log($"{slime.Name} took {damage} damage!");

        if (health.Health <= 0)
        {
            Die();
            Debug.Log($"{slime.Name} has died!");
        }
    }

    protected override void Defend()
    {
        isDefending = true;
        EndTurn();
    }

    protected override void Die()
    {
        EntityHealthBar healthBar = GetComponent<EntityHealthBar>();
        Destroy(healthBar.BarFullObj);
        Destroy(healthBar.BarEmptyObj);
        Destroy(this.gameObject);
    }

    public override void DecideMove()
    {
        if (Random.Range(0, 1) < 0.5)
            DealDamage(FindObjectOfType<PlayerCombat>());
        else
            Defend();
    }
}
