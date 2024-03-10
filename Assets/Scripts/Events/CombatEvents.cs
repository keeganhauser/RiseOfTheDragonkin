using System;
using System.Xml.Linq;
using UnityEngine;

public class CombatEvents
{
    // Called when combat first starts
    public event Action<Enemy> onCombatStart;
    public void CombatStart(Enemy enemy)
    {
        onCombatStart?.Invoke(enemy);
    }

    // Called when combat ends
    public event Action onCombatEnd;
    public void CombatEnd()
    {
        onCombatEnd?.Invoke();
    }

    // Called when something dies in combat
    public event Action<CombatController> onEntityDeath;
    public void EntityDied(CombatController controller)
    {
        onEntityDeath?.Invoke(controller);
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

    public event Action<string> onCombatStatusChange;
    public void CombatStatusChange(string status)
    {
        onCombatStatusChange?.Invoke(status);
    }
}
