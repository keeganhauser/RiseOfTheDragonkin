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
    private Transform playerBattleStation;

    [SerializeField]
    private Transform enemyBattleStation;

    public static BattleSystem Instance;

    public static Enemy enemy;
    private Unit playerUnit;
    private Unit enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        // Spawn player
        Player.Instance.transform.position = playerBattleStation.position;
        Player.Instance.CanMove = false;

        playerUnit = Player.Instance.GetComponent<Unit>();
        playerUnit.OnDeath.AddListener(EndBattle);
        playerHUD.RegisterUnit(playerUnit);

        // Spawn enemy
        GameObject enemyObj = EnemySpawner.SpawnEnemy(enemy, enemyBattleStation.position, Quaternion.identity, enemyBattleStation);
        enemyUnit = enemyObj.GetComponent<Unit>();
        enemyUnit.OnDeath.AddListener(EndBattle);
        enemyHUD.RegisterUnit(enemyUnit);

        dialogueText.text = $"You've encountered a hostile {enemy.enemyName}!";

        playerHUD.SetHUD();
        enemyHUD.SetHUD();

        yield return new WaitForSeconds(3f);

        state = BattleState.PlayerTurn;
        PlayerTurn();   
    }

    private IEnumerator PlayerAttack()
    {
        // If the unit dies, then EndBattle will be called.
        playerUnit.DealDamage(enemyUnit);

        dialogueText.text = $"Attacked {enemy.enemyName} for {playerUnit.Damage}!";

        yield return new WaitForSeconds(2f);

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        enemyUnit.IsDefending = false;
        
        // Figure out what move the enemy makes

        dialogueText.text = $"{enemy.enemyName} attacked!";


        yield return new WaitForSeconds(1f);

        enemyUnit.DealDamage(playerUnit);

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
