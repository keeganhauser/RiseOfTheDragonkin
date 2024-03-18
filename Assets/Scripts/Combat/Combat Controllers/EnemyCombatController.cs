using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : CombatController
{
    [SerializeField] private int level;
    public override int Level => level;

    public override string Name => gameObject.name;

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onEnemyTurn += HandleTurn;
        GameEventsManager.Instance.PlayerEvents.onPlayerAttack += TakeDamage;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onEnemyTurn -= HandleTurn;
        GameEventsManager.Instance.PlayerEvents.onPlayerAttack -= TakeDamage;
    }

    protected override void HandleTurn()
    {
        Debug.Log("Handling enemy turn");

        GameEventsManager.Instance.EnemyEvents.EnemyDecideAttack();
        Attack();
    }

    protected override void Attack()
    {
        GameEventsManager.Instance.EnemyEvents.EnemyAttack(damage);
        GameEventsManager.Instance.CombatEvents.EndTurn();
    }
}
