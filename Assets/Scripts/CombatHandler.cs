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

    [SerializeField]
    private GameObject enemyPrefab;
    
    public GameObject Enemy { get; private set; }

    void Start()
    {
        CurrentTurn = Turn.Player;
        EnterCombat();
    }

    void Update()
    {

    }

    public void EnterCombat()
    {
        Debug.Log("Entering combat!");

        // Setup player
        Player.Instance.CanMove = false;
        Player.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Player.Instance.transform.position = playerPosition;

        // Spawn enemy
        Enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
    }

    public void EndCombat()
    {
        // Hide UI
        FindObjectOfType<PlayerCombatMenu>().DisableCombatMenu();
        Player.Instance.CanMove = true;
        SceneManager.LoadScene("SampleScene");
    }

    private void InvokeEnemyTurn()
    {
        try
        {
            Enemy.GetComponent<EnemyCombat>().DecideMove();
        } catch (MissingReferenceException)
        {
            EndCombat();
        }
    }

    public void EndTurn()
    {
        // Player has chosen an action, end their turn.
        if (CurrentTurn == Turn.Player)
        {
            // Hide UI
            FindObjectOfType<PlayerCombatMenu>().DisableCombatMenu();

            if (Enemy.transform == null)
            {
                Debug.Log("ENEMY NULL");
                EndCombat();
                return;
            }

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
