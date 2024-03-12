using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState
{
    NotStarted,
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    
    public Enemy enemy; // TODO: Make private again
    
    private bool combatEnded;

    public CombatController playerCombatController;
    public CombatController enemyCombatController;

    private BattleState battleState;

    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private Vector2 enemyPosition;

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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        combatEnded = false;
        battleState = BattleState.NotStarted;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //GameEventsManager.Instance.CombatEvents.CombatTrigger(enemy);
            CustomSceneManager.Instance.FadeAndLoadScene("CombatScene", Vector3.zero);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CustomSceneManager.Instance.FadeAndLoadScene("OverworldArea1", Vector3.zero);
        }

    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatTrigger += CombatTrigger;
        GameEventsManager.Instance.CombatEvents.onLoseCombat += EndCombat;
        GameEventsManager.Instance.CombatEvents.onEndTurn += EndTurn;
        GameEventsManager.Instance.CombatEvents.onEnemyChange += SetEnemy;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatTrigger -= CombatTrigger;
        GameEventsManager.Instance.CombatEvents.onLoseCombat -= EndCombat;
        GameEventsManager.Instance.CombatEvents.onEndTurn -= EndTurn;
        GameEventsManager.Instance.CombatEvents.onEnemyChange -= SetEnemy;
    }

    private void CombatTrigger(Enemy enemy)
    {
        if (enemy == null)
        {
            Debug.LogWarning("Tried to start combat but enemy is null!");
            return;
        }

        if (battleState != BattleState.NotStarted) return;
        StartCoroutine(InitializeCombat(enemy));
    }

    private IEnumerator InitializeCombat(Enemy enemy)
    {
        // Set battle state
        battleState = BattleState.Start;
        Status = $"You've encountered a {enemy.enemyName}!";

        SetEnemy(enemy);

        GameEventsManager.Instance.CombatEvents.CombatPreInitialization();
        
        yield return StartCoroutine(SetupCombat());

        GameEventsManager.Instance.CombatEvents.CombatPostInitialization();

        yield return StartCoroutine(DoCombat());
    }

    private IEnumerator SetupCombat()
    {
        // Load combat scene
        //SceneManager.LoadScene("CombatScene");
        yield return new WaitForFixedUpdate();


        // Get components
        playerCombatController = Player.Instance.GetComponent<CombatController>();

        // Move player to proper spot
        Player.Instance.gameObject.transform.position = playerPosition;

        // Disable player movement and interactions
        //GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();

        // Spawn enemy
        //GameObject enemyObj = EnemySpawner.SpawnEnemy(enemy, enemyBattleStation.position, Quaternion.identity, enemyBattleStation);
        GameObject enemyObj = EnemySpawner.SpawnEnemy(enemy, enemyPosition, Quaternion.identity, null);
        enemyCombatController = enemyObj.GetComponent<CombatController>();
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
        if (battleState == BattleState.PlayerTurn)
        {
            battleState = BattleState.EnemyTurn;
        }
        else
        {
            battleState = BattleState.PlayerTurn;
        }
    }

    private void EndCombat(CombatController losingController)
    {
        combatEnded = true;
        StopAllCoroutines();

        // Show popup
        if (losingController == playerCombatController)
        {
            Debug.Log("Player has lost!");
            Status = "You've been slain...";
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

        GameEventsManager.Instance.CombatEvents.CombatEnd();

        battleState = BattleState.NotStarted;

        // Go back to overworld
        yield return new WaitForSeconds(0.5f);
        //GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
    }
}
