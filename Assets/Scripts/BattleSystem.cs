using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject enemyPrefab;

    private Transform playerBattleStation;
    private Transform enemyBattleStation;

    private Unit playerUnit;
    private Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        GameObject playerObj = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerObj.GetComponent<Unit>();
        playerUnit.OnDeath.AddListener(EndBattle);

        GameObject enemyObj = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyObj.GetComponent<Unit>();
        enemyUnit.OnDeath.AddListener(EndBattle);

        dialogueText.text = $"You've encountered a hostile {enemyUnit.UnitName}!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PlayerTurn;
        PlayerTurn();   
    }

    private IEnumerator PlayerAttack()
    {
        // If the unit dies, then EndBattle will be called.
        enemyUnit.TakeDamage(playerUnit.Damage);

        dialogueText.text = $"Attacked {enemyUnit.UnitName} for {playerUnit.Damage}!";

        yield return new WaitForSeconds(2f);

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        dialogueText.text = $"{enemyUnit.UnitName} attacked!";

        yield return new WaitForSeconds(1f);

        playerUnit.TakeDamage(enemyUnit.Damage);

        yield return new WaitForSeconds(1f);

        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    private void EndBattle()
    {
        // If the battle ended during the player's turn, player wins
        if (state == BattleState.PlayerTurn)
        {
            dialogueText.text = "You won!";
        }
        else if (state == BattleState.EnemyTurn)
        {
            dialogueText.text = "You were slain...";
        }
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    private IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        dialogueText.text = "You regained 5 health!";

        yield return new WaitForSeconds(2f);

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state == BattleState.PlayerTurn)
        {
            StartCoroutine(PlayerAttack());
        }
    }

    public void OnHealButton()
    {
        if (state == BattleState.PlayerTurn)
        {
            StartCoroutine(PlayerHeal());
        }
    }
}
