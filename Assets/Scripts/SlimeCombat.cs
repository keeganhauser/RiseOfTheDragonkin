using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCombat : EnemyCombat
{
    private Slime slime;

    private void Start()
    {
        slime = GetComponent<Slime>();
    }

    public override void TakeDamage(int damage)
    {
        if (isDefending)
        {
            damage /= 2;
            isDefending = false;
        }
        slime.Health -= damage;
        Debug.Log($"{slime.Name} took {damage} damage!");

        if (slime.Health <= 0)
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
