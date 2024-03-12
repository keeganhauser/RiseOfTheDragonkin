using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : CombatController
{    
    public override int Level => 1;
    public override string Name => GetComponent<Entity>().Name;

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onPlayerTurn += HandleTurn;
        GameEventsManager.Instance.CombatEvents.onAttack += TakeDamage;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onPlayerTurn -= HandleTurn;
        GameEventsManager.Instance.CombatEvents.onAttack -= TakeDamage;
    }

    protected override void HandleTurn()
    {
        Debug.Log("Handling player turn");
    }

    public void ButtonAttack()
    {
        Attack(CombatManager.Instance.enemyCombatController);
    }

    public void ButtonDefend()
    {
        Defend();
    }

    public void ButtonItem()
    {

    }

    public void ButtonEscape()
    {

    }
}
