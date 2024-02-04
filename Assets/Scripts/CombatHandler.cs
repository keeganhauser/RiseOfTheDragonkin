using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CombatHandler : MonoBehaviour
{
    public enum Turn
    {
        Player,
        Enemy
    }

    public Turn CurrentTurn { get; private set; }
    private Vector2 playerPosition = new Vector2(-3.5f, -0.5f);
    private Vector2 enemyPosition = new Vector2(3.5f, -0.5f);
    public EnemyCombat[] Enemies { get; private set; }

    void Start()
    {
        CurrentTurn = Turn.Player;

        Invoke(nameof(EnterCombat), 1f);
    }

    void Update()
    {

    }

    public void EnterCombat()
    {
        Debug.Log("Entering combat!");
        Enemies = FindObjectsByType<EnemyCombat>(FindObjectsSortMode.None);
        Debug.Log($"Found {Enemies.Length} combat objects.");
        Player.Instance.CanMove = false;
    }

    public void EndCombat()
    {
        // Hide UI
        FindObjectOfType<PlayerCombatMenu>().DisableCombatMenu();
        Player.Instance.CanMove = true;
    }

    private void InvokeEnemyTurn()
    {
        foreach (EnemyCombat enemy in Enemies)
        {
            enemy.DecideMove();
        }
    }

    public void EndTurn()
    {
        // Player has chosen an action, end their turn.
        if (CurrentTurn == Turn.Player)
        {
            // Hide UI
            FindObjectOfType<PlayerCombatMenu>().DisableCombatMenu();

            CurrentTurn = Turn.Enemy;
            Invoke(nameof(InvokeEnemyTurn), 1f);
        }
        else
        {
            // Show UI
            FindObjectOfType<PlayerCombatMenu>().EnableCombatMenu();
            CurrentTurn = Turn.Player;
        }
    }
}
