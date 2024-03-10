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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);

        combatEnded = false;
        battleState = BattleState.NotStarted;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameEventsManager.Instance.CombatEvents.CombatStart(enemy);
        }

    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatStart += CombatStart;
        GameEventsManager.Instance.CombatEvents.onEntityDeath += EndCombat;
        GameEventsManager.Instance.CombatEvents.onEndTurn += EndTurn;
        GameEventsManager.Instance.CombatEvents.onEnemyChange += SetEnemy;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatStart -= CombatStart;
        GameEventsManager.Instance.CombatEvents.onEntityDeath -= EndCombat;
        GameEventsManager.Instance.CombatEvents.onEndTurn -= EndTurn;
        GameEventsManager.Instance.CombatEvents.onEnemyChange -= SetEnemy;
    }

    private void CombatStart(Enemy enemy)
    {
        if (enemy == null)
        {
            Debug.LogWarning("Tried to start combat but enemy is null!");
            return;
        }

        if (battleState != BattleState.NotStarted) return;


        battleState = BattleState.Start;
        Debug.Log("Starting combat");

        SetEnemy(enemy);
        StartCoroutine(InitializeCombat());
        StartCoroutine(DoCombat());

    }

    private IEnumerator InitializeCombat()
    {
        // Load combat scene
        SceneManager.LoadScene("CombatScene");
        yield return new WaitForFixedUpdate();

        // Set battle state

        // Get components
        playerCombatController = Player.Instance.GetComponent<CombatController>();

        // Move player to proper spot

        // Disable player movement and interactions
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();

        // Spawn enemy
        //GameObject enemyObj = EnemySpawner.SpawnEnemy(enemy, enemyBattleStation.position, Quaternion.identity, enemyBattleStation);
        GameObject enemyObj = EnemySpawner.SpawnEnemy(enemy);
        enemyCombatController = enemyObj.GetComponent<CombatController>();
    }

    private void SetEnemy(Enemy enemy)
    {
        if (battleState == BattleState.Start)
            this.enemy = enemy;
    }

    private IEnumerator DoCombat()
    {
        // Start with the player going first
        battleState = BattleState.PlayerTurn;

        // While combatEnded is false
        while (!combatEnded)
        {
            // Start player's turn
            GameEventsManager.Instance.CombatEvents.PlayerTurn();

            while (battleState == BattleState.PlayerTurn)
                yield return null;


            yield return new WaitForSeconds(3f);

            // Start enemy's turn
            GameEventsManager.Instance.CombatEvents.EnemyTurn();

            while (battleState == BattleState.EnemyTurn) 
                yield return null;
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

    private void EndCombat(CombatController controller)
    {
        combatEnded = true;
        StopAllCoroutines();

        // Show popup
        if (controller == playerCombatController)
            Debug.Log("Player has lost!");
        else
            Debug.Log("Player has won!");

        // Go back to overworld
    }
}
