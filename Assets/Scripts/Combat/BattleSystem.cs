//    [SerializeField]
//    private Transform playerBattleStation;

//    [SerializeField]
//    private Transform enemyBattleStation;

//    [SerializeField]
//    private RectTransform actionButtonPanel;

//    public static BattleSystem Instance;

//    public BattleHUD playerHUD;
//    public BattleHUD enemyHUD;

//    public BattleState state;

//    private bool combatInProgress;

//    private Coroutine combatRoutine;

//    private void Awake()
//    {
//        InitializeBattleSystem();
//        actionButtonPanel.gameObject.SetActive(false);
//    }

//    public void InitializeBattleSystem()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//            Destroy(this.gameObject);
//    }

//    void Start()
//    {
//        state = BattleState.Start;
//        //this.enemy = CombatManager.Instance.enemy;
//        StartCoroutine(SetupBattle(enemy));
//    }

//    public IEnumerator SetupBattle(Enemy enemy)
//    {
//        AudioManager.Instance.PlayMusic(GameState.Combat);

//        // Spawn player
//        Player.Instance.transform.position = playerBattleStation.position;
//        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();

//        playerUnit = Player.Instance.GetComponent<Unit>();
//        playerUnit.UnitName = Player.Instance.Name;
//        playerUnit.OnDeath.AddListener(EndCombatCallback);
//        playerHUD.RegisterUnit(playerUnit);

//        // Spawn enemy
//        GameObject enemyObj = EnemySpawner.SpawnEnemy(enemy, enemyBattleStation.position, Quaternion.identity, enemyBattleStation);
//        enemyUnit = enemyObj.GetComponent<Unit>();
//        enemyUnit.UnitName = enemy.enemyName;
//        enemyUnit.OnDeath.AddListener(EndCombatCallback);
//        enemyHUD.RegisterUnit(enemyUnit);

//        dialogueText.text = $"You've encountered a hostile {enemy.enemyName}!";

//        playerHUD.SetHUD();
//        enemyHUD.SetHUD();

//        yield return new WaitForSeconds(3f);

//        state = BattleState.PlayerTurn;
//        combatInProgress = true;
//        StartCoroutine(RunBattle());
//    }


//    private IEnumerator HandleUnitAction(Unit main, Unit other)
//    {
//        string actionStr = string.Empty;

//        switch (main.combatAction)
//        {
//            case CombatAction.Attack:
//                main.DealDamage(other);
//                if (main == playerUnit)
//                    //StartCoroutine(Player.Instance.TriggerAnimation(CharacterAction.Attack));
//                actionStr = $"{main.UnitName} attacked {other.UnitName} for {main.Damage} damage!";
//                break;
//            case CombatAction.Defend:
//                main.IsDefending = true;
//                actionStr = $"{main.UnitName} blocked!";
//                break;
//            case CombatAction.Item:
//                // TODO: Use item in battle
//                break;
//            case CombatAction.Escape:
//                // TODO: Escape from battle
//                break;
//            default:
//                break;
//        }

//        dialogueText.text = actionStr;
//        main.combatAction = null;
//        yield return new WaitForSeconds(2f);
//    }


//    private IEnumerator EndCombat()
//    {
//        combatInProgress = false;

//        StopCoroutine(combatRoutine);
//        yield return new WaitForSeconds(3f);

//        Debug.Log("COMBAT ENDED");

//        if (playerUnit.CurrentHealth > 0)
//        {
//            Debug.Log("Player wins");
//            dialogueText.text = "You won!";
//        }
//        else
//        {
//            Debug.Log("Enemy wins");
//            dialogueText.text = "You were slain...";
//        }
//    }
