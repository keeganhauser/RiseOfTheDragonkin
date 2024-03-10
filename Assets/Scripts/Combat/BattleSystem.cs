using System.Collections;
using TMPro;
using UnityEngine;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public enum CombatAction
{
    Attack,
    Defend,
    Item,
    Escape
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private Transform playerBattleStation;

    [SerializeField]
    private Transform enemyBattleStation;

    [SerializeField]
    private RectTransform actionButtonPanel;

    public static BattleSystem Instance;

    private Enemy enemy;
    private Unit playerUnit;
    private Unit enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    private bool combatInProgress;

    private Coroutine combatRoutine;

    private void Awake()
    {
        InitializeBattleSystem();
        actionButtonPanel.gameObject.SetActive(false);
    }

    public void InitializeBattleSystem()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        state = BattleState.Start;
        this.enemy = CombatManager.Instance.enemy;
        StartCoroutine(SetupBattle(enemy));
    }

    public IEnumerator SetupBattle(Enemy enemy)
    {
        AudioManager.Instance.PlayMusic(GameState.Combat);

        // Spawn player
        Player.Instance.transform.position = playerBattleStation.position;
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();

        playerUnit = Player.Instance.GetComponent<Unit>();
        playerUnit.UnitName = Player.Instance.Name;
        playerUnit.OnDeath.AddListener(EndCombatCallback);
        playerHUD.RegisterUnit(playerUnit);

        // Spawn enemy
        GameObject enemyObj = EnemySpawner.SpawnEnemy(enemy, enemyBattleStation.position, Quaternion.identity, enemyBattleStation);
        enemyUnit = enemyObj.GetComponent<Unit>();
        enemyUnit.UnitName = enemy.enemyName;
        enemyUnit.OnDeath.AddListener(EndCombatCallback);
        enemyHUD.RegisterUnit(enemyUnit);

        dialogueText.text = $"You've encountered a hostile {enemy.enemyName}!";

        playerHUD.SetHUD();
        enemyHUD.SetHUD();

        yield return new WaitForSeconds(3f);

        state = BattleState.PlayerTurn;
        combatInProgress = true;
        StartCoroutine(RunBattle());
    }

    private IEnumerator RunBattle()
    {
        while (combatInProgress)
        {
            yield return combatRoutine = StartCoroutine(PlayTurn());
        }

    }

    private IEnumerator PlayTurn()
    {
        Debug.Log("Start turn");

        // Show action buttons
        actionButtonPanel.gameObject.SetActive(true);

        // Wait for player to choose an action
        while (playerUnit.combatAction == null)
        {
            dialogueText.text = "Choose an action:";
            yield return null;
        }

        // Hide action buttons
        actionButtonPanel.gameObject.SetActive(false);

        // If player selected to use an item or escape, do those first before
        // trying to figure out enemy moves.
        if (playerUnit.combatAction == CombatAction.Item)
        {
            // TODO: Use an item
        }
        else if (playerUnit.combatAction == CombatAction.Escape)
        {
            // TODO: Escape
        }

        yield return new WaitForSeconds(1f);

        // Have enemy decide action based off its behavior
        enemyUnit.DecideCombatAction();

        // Figure out turn order from speed of units
        Unit[] unitsInOrder = GetTurnOrder();

        // Execute actions
        yield return StartCoroutine(HandleUnitAction(unitsInOrder[0], unitsInOrder[1]));
        yield return StartCoroutine(HandleUnitAction(unitsInOrder[1], unitsInOrder[0]));
    }

    private Unit[] GetTurnOrder()
    {
        Unit[] units;
        if (playerUnit.Speed > enemyUnit.Speed)
        {
            units = new Unit[2] { playerUnit, enemyUnit };
        }
        else if (playerUnit.Speed < enemyUnit.Speed)
        {
            units = new Unit[2] { enemyUnit, playerUnit };
        }
        else
        {
            if (Random.value <= 0.5f)
            {
                units = new Unit[2] { playerUnit, enemyUnit };
            }
            else
            {
                units = new Unit[2] { enemyUnit, playerUnit };
            }
        }
        return units;
    }

    private IEnumerator HandleUnitAction(Unit main, Unit other)
    {
        string actionStr = string.Empty;

        switch (main.combatAction)
        {
            case CombatAction.Attack:
                main.DealDamage(other);
                if (main == playerUnit)
                    //StartCoroutine(Player.Instance.TriggerAnimation(CharacterAction.Attack));
                actionStr = $"{main.UnitName} attacked {other.UnitName} for {main.Damage} damage!";
                break;
            case CombatAction.Defend:
                main.IsDefending = true;
                actionStr = $"{main.UnitName} blocked!";
                break;
            case CombatAction.Item:
                // TODO: Use item in battle
                break;
            case CombatAction.Escape:
                // TODO: Escape from battle
                break;
            default:
                break;
        }

        dialogueText.text = actionStr;
        main.combatAction = null;
        yield return new WaitForSeconds(2f);
    }

    private void EndCombatCallback()
    {
        StartCoroutine(EndCombat());
    }

    private IEnumerator EndCombat()
    {
        combatInProgress = false;

        StopCoroutine(combatRoutine);
        yield return new WaitForSeconds(3f);

        Debug.Log("COMBAT ENDED");

        if (playerUnit.CurrentHealth > 0)
        {
            Debug.Log("Player wins");
            dialogueText.text = "You won!";
        }
        else
        {
            Debug.Log("Enemy wins");
            dialogueText.text = "You were slain...";
        }
    }

    public void OnAttackButton()
    {
        playerUnit.combatAction = CombatAction.Attack;
    }

    public void OnDefendButton()
    {
        playerUnit.combatAction = CombatAction.Defend;
    }
}
