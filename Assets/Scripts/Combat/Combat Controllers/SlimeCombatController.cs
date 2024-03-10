using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCombatController : CombatController
{
    [SerializeField] private int level;
    public override int Level => level;

    public override string Name => "Slime";

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onEnemyTurn += HandleTurn;
        GameEventsManager.Instance.CombatEvents.onAttack += TakeDamage;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onEnemyTurn -= HandleTurn;
        GameEventsManager.Instance.CombatEvents.onAttack -= TakeDamage;
    }

    protected override void HandleTurn()
    {
        Debug.Log("Handling enemy turn");

        Attack(CombatManager.Instance.playerCombatController);
    }
}
