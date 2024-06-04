using UnityEngine;
using UnityEngine.U2D.Animation;

public class Player : SingletonMonoBehavior<Player>
{
    [SerializeField] public string Name;
    [SerializeField] private PlayerClass playerClass;
    [SerializeField] private PlayerClass defaultPlayerClass;
    [SerializeField] private PlayerStats stats;
    private PlayerHealth health;
    private PlayerMana mana;
    private PlayerExperience exp;

    public PlayerStats Stats => stats;
    public PlayerClass ClassInfo => playerClass;

    protected override void Awake()
    {
        base.Awake();

        if (playerClass == null)
            InitializePlayer(defaultPlayerClass);
        else
            InitializePlayer(playerClass);

        health = GetComponent<PlayerHealth>();
        mana = GetComponent<PlayerMana>();
        exp = GetComponent<PlayerExperience>();
    }

    private void Start()
    {
        GameEventsManager.Instance.PlayerEvents.PlayerInitializeFinish();
    }

    public void InitializePlayer(PlayerClass playerClass)
    {
        this.playerClass = playerClass;

        GetComponentInChildren<SpriteLibrary>().spriteLibraryAsset = playerClass.spriteLibrary;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.TakeDamage(1f);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mana.UseMana(1f);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            exp.AddExp(300f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }
    }

    private void ResetPlayer()
    {
        stats.ResetPlayer();
        GameEventsManager.Instance.PlayerEvents.PlayerRevive();
    }
}
