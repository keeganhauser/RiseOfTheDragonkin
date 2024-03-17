using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : SingletonMonoBehavior<CombatManager>
{   
    public Enemy enemy; // TODO: Make private again
    
    private bool combatEnded;

    public CombatController playerCombatController;
    public CombatController enemyCombatController;

    private BattleState battleState;

    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private Vector2 enemyPosition;

    [SerializeField] private Transform playerBattleStation;
    [SerializeField] private Transform enemyBattleStation;

    private string previousSceneName;
    private Vector3 previousPlayerPosition;
    private Coroutine combatRoutine;
    private Coroutine initRoutine;

    private string status;
    private string Status
    {
        get => status;
        set
        {
            status = value;
            if (!string.IsNullOrEmpty(status))
                GameEventsManager.Instance.CombatEvents.CombatStatusChange(status);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        combatEnded = false;
        battleState = BattleState.NotStarted;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            GameEventsManager.Instance.CombatEvents.CombatTrigger(enemy);
        }

    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatTrigger += CombatTrigger;
        GameEventsManager.Instance.CombatEvents.onLoseCombat += EndCombat;
        GameEventsManager.Instance.CombatEvents.onEndTurn += EndTurn;
        GameEventsManager.Instance.CombatEvents.onEnemyChange += SetEnemy;
        GameEventsManager.Instance.CombatEvents.onEscape += Escape;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatTrigger -= CombatTrigger;
        GameEventsManager.Instance.CombatEvents.onLoseCombat -= EndCombat;
        GameEventsManager.Instance.CombatEvents.onEndTurn -= EndTurn;
        GameEventsManager.Instance.CombatEvents.onEnemyChange -= SetEnemy;
        GameEventsManager.Instance.CombatEvents.onEscape -= Escape;
    }

    private void CombatTrigger(Enemy enemy)
    {
        if (enemy == null)
        {
            Debug.LogWarning("Tried to start combat but enemy is null!");
            return;
        }

        if (battleState != BattleState.NotStarted) return;

        Debug.Log("Start init combat");
        GameEventsManager.Instance.CombatEvents.CombatPrePreInitialization();

        // Save the scene in which the player was previously located
        previousSceneName = SceneManager.GetActiveScene().name;
        GameEventsManager.Instance.SceneEvents.onSceneSwitchEnd += StartCombat;
        CustomSceneManager.Instance.FadeAndLoadScene(SceneName.CombatScene.ToString(), Vector3.zero);
    }

    private void StartCombat()
    {
        GameEventsManager.Instance.SceneEvents.onSceneSwitchEnd -= StartCombat;
        initRoutine = StartCoroutine(InitializeCombat(enemy));
    }

    private IEnumerator InitializeCombat(Enemy enemy)
    {
        // Set battle state
        combatEnded = false;
        battleState = BattleState.Start;
        Status = $"You've encountered a {enemy.enemyName}!";

        SetEnemy(enemy);

        GameEventsManager.Instance.CombatEvents.CombatPreInitialization();
        
        SetupCombat();

        GameEventsManager.Instance.CombatEvents.CombatPostInitialization();

        combatRoutine = StartCoroutine(DoCombat());
        yield return combatRoutine;
    }

    private void SetupCombat()
    {
        // Get components
        playerCombatController = Player.Instance.GetComponent<CombatController>();

        // Move player to proper spot
        Player.Instance.transform.position = playerBattleStation.position;

        // Disable player movement and interactions
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();

        // Spawn enemy
        GameObject enemyObj = EnemySpawner.SpawnEnemy(enemy, enemyBattleStation.position, Quaternion.identity, enemyBattleStation);
        enemyCombatController = enemyObj.GetComponent<CombatController>();
        Debug.Log($"ecc get: {enemyCombatController.GetInstanceID()}");
    }

    private void SetEnemy(Enemy enemy)
    {
        if (battleState == BattleState.Start)
            this.enemy = enemy;
    }

    private IEnumerator DoCombat()
    {
        GameEventsManager.Instance.CombatEvents.CombatStart();
        // Start with the player going first
        battleState = BattleState.PlayerTurn;

        // While combatEnded is false
        while (!combatEnded)
        {
            // Start player's turn
            Status = "Choose an action:";
            GameEventsManager.Instance.CombatEvents.PlayerTurn();

            while (battleState == BattleState.PlayerTurn)
                yield return null;


            yield return new WaitForSeconds(1.5f);

            // Start enemy's turn
            GameEventsManager.Instance.CombatEvents.EnemyTurn();

            while (battleState == BattleState.EnemyTurn) 
                yield return null;

            yield return new WaitForSeconds(1.5f);
        }
    }

    private void EndTurn()
    {
        if (combatEnded) return;
        if (battleState == BattleState.PlayerTurn)
        {
            battleState = BattleState.EnemyTurn;
        }
        else
        {
            battleState = BattleState.PlayerTurn;
        }
    }

    private void Escape(CombatController escapingController)
    {
        EndCombat(escapingController);
    }

    private void EndCombat(CombatController losingController)
    {
        StopCoroutine(combatRoutine);
        StopCoroutine(initRoutine);
        combatEnded = true;
        battleState = BattleState.NotStarted;

        // Show popup
        if (losingController == playerCombatController)
        {
            Debug.Log("Player has lost!");
            Status = "You've lost...";
        }
        else
        {
            Debug.Log("Player has won!");
            Status = $"{losingController.Name} has been defeated!";
        }

        StartCoroutine(CombatEndRoutine());
    }

    // Function for anything that needs to happen before combat scene ends
    private IEnumerator CombatEndRoutine()
    {
        yield return new WaitForSeconds(3f);

        
        GameEventsManager.Instance.SceneEvents.onSceneSwitchStart += CleanUpCombat;
        CustomSceneManager.Instance.FadeAndLoadScene(previousSceneName, previousPlayerPosition);
    }

    private void CleanUpCombat()
    {
        GameEventsManager.Instance.SceneEvents.onSceneSwitchStart -= CleanUpCombat;
        GameEventsManager.Instance.CombatEvents.CombatEnd();
        GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
        foreach (Transform child in enemyBattleStation.transform)
            Destroy(child.gameObject);
    }
}
