using System;
using System.Xml.Linq;
using UnityEngine;

public class CombatEvents
{
    // Called when combat is triggered
    public event Action<Enemy> onCombatTrigger;
    public void CombatTrigger(Enemy enemy)
    {
        onCombatTrigger?.Invoke(enemy);
    }

    public event Action onCombatPrePreInitialization;
    public void CombatPrePreInitialization()
    {
        onCombatPrePreInitialization?.Invoke();
    }

    // Called before combat starts being initialized
    public event Action onCombatPreInitialization;
    public void CombatPreInitialization()
    {
        onCombatPreInitialization?.Invoke();
    }

    // Called after combat is initialized
    public event Action onCombatPostInitialization;
    public void CombatPostInitialization()
    {
        Debug.Log("Combat post init event");
        onCombatPostInitialization?.Invoke();
    }

    // Called when combat starts
    public event Action onCombatStart;
    public void CombatStart()
    {
        onCombatStart?.Invoke();
    }

    // Called when combat ends
    public event Action onCombatEnd;
    public void CombatEnd()
    {
        Debug.Log("Combat end");
        onCombatEnd?.Invoke();
    }

    // Called when something dies in combat
    public event Action<CombatController> onLoseCombat;
    public void LoseCombat(CombatController controller)
    {
        onLoseCombat?.Invoke(controller);
    }

    // Called when it is the player's turn
    public event Action onPlayerTurn;
    public void PlayerTurn()
    {
        onPlayerTurn?.Invoke();
    }

    // Called when it is the enemy's turn
    public event Action onEnemyTurn;
    public void EnemyTurn()
    {
        onEnemyTurn?.Invoke();
    }

    // Called when something ends their turn
    public event Action onEndTurn;
    public void EndTurn()
    {
        Debug.Log("Combat end turn");
        onEndTurn?.Invoke();
    }

    // Called when an enemy is to be changed for combat
    public event Action<Enemy> onEnemyChange;
    public void EnemyChange(Enemy enemy)
    {
        onEnemyChange?.Invoke(enemy);
    }

    // Called when something attacks
    public event Action<CombatController, int> onAttack;
    public void Attack(CombatController controller, int amount)
    {
        onAttack?.Invoke(controller, amount);
    }

    // Called when something defends
    public event Action<CombatController> onDefend;
    public void Defend(CombatController controller)
    {
        onDefend?.Invoke(controller);
    }

    public event Action<CombatController> onEscape;
    public void Escape(CombatController controller)
    {
        onEscape?.Invoke(controller);
    }

    public event Action<string> onCombatStatusChange;
    public void CombatStatusChange(string status)
    {
        onCombatStatusChange?.Invoke(status);
    }

    public event Action<CombatController> onHealthChange;
    public void HealthChange(CombatController controller)
    {
        onHealthChange?.Invoke(controller);
    }
}
