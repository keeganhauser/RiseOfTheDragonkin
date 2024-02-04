using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combat : MonoBehaviour
{
    [SerializeField]
    protected int damage = 10;
    protected bool inCombat;
    protected bool isDefending;

    protected virtual void DealDamage(Combat entityCombat)
    {
        entityCombat.TakeDamage(damage);
        EndTurn();
    }

    protected abstract void Defend();
    public abstract void TakeDamage(int damage);
    protected abstract void Die();

    protected void EndTurn()
    {
        FindObjectOfType<CombatHandler>().EndTurn();
    }
}
