using System.Collections;
using System.Collections.Generic;
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
    private Enemy enemy;
    void Start()
    {
        CurrentTurn = Turn.Player;
        EnterCombat(FindObjectOfType<Enemy>());
    }

    void Update()
    {

    }

    public void EnterCombat(Enemy enemy)
    {
        Debug.Log($"{Player.Instance.Name} has entered combat with {enemy._name}!");
        Player.Instance.transform.position = playerPosition;
        Player.Instance.ResetPosition();
        enemy.transform.position = enemyPosition;
        this.enemy = enemy;
        this.enemy.InitiateCombat(this);
    }

    public void EndCombat()
    {
        // Hide UI
        FindObjectOfType<PlayerCombatMenu>().DisableCombatMenu();

        Destroy(enemy.gameObject);

        Player.Instance.LeaveCombat();
        DontDestroyOnLoad(Player.Instance);

        // Go back to previous scene
        // TODO: Remove hardcoded values
        SceneManager.LoadScene("SampleScene");
    }

    public void EndTurn()
    {
        // Player has chosen an action, end their turn.
        if (CurrentTurn == Turn.Player)
        {
            // Hide UI
            FindObjectOfType<PlayerCombatMenu>().DisableCombatMenu();
            CurrentTurn = Turn.Enemy;
            
        }
        else
        {
            // Show UI
            FindObjectOfType<PlayerCombatMenu>().EnableCombatMenu();

            CurrentTurn = Turn.Player;
        }
    }
}
