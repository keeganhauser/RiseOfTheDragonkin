using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    private Transform playerBattleStation;

    [SerializeField]
    private Transform enemyBattleStation;

    private Unit playerUnit;
    private Unit enemyUnit;

    public TMP_Text dialogueText;

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
        GameObject playerObj = Instantiate(playerPrefab, playerBattleStation.position, Quaternion.identity, playerBattleStation);
        Player.Instance.CanMove = false;
        playerUnit = playerObj.GetComponent<Unit>();
        playerUnit.OnDeath.AddListener(EndBattle);
        playerHUD.RegisterUnit(playerUnit);

        GameObject enemyObj = Instantiate(enemyPrefab, enemyBattleStation.position, Quaternion.identity, enemyBattleStation);
        enemyUnit = enemyObj.GetComponent<Unit>();
        enemyUnit.OnDeath.AddListener(EndBattle);
        enemyHUD.RegisterUnit(enemyUnit);

        dialogueText.text = $"You've encountered a hostile {enemyUnit.UnitName}!";

        playerHUD.SetHUD();
        enemyHUD.SetHUD();

        yield return new WaitForSeconds(3f);

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
        enemyUnit.IsDefending = false;
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
        Player.Instance.CanMove = true;
    }

    private void PlayerTurn()
    {
        playerUnit.IsDefending = false;
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
