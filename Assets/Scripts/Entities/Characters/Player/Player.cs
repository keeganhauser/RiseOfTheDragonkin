using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class Player : SingletonMonoBehavior<Player>
{
    [SerializeField] public string Name;
    [SerializeField] private PlayerClass playerClass;
    [SerializeField] private PlayerClass defaultPlayerClass;

    protected override void Awake()
    {
        base.Awake();

        if (playerClass == null)
            InitializePlayer(defaultPlayerClass);
        else
            InitializePlayer(playerClass);
        GameEventsManager.Instance.PlayerEvents.PlayerInitializeFinish();
    }

    public void InitializePlayer(PlayerClass playerClass)
    { 
        this.playerClass = playerClass;
        GetComponent<PlayerCombatController>().Initialize(
            playerClass.health,
            playerClass.mana,
            playerClass.damage,
            playerClass.speed);

        GetComponentInChildren<SpriteLibrary>().spriteLibraryAsset = playerClass.spriteLibrary;
    }
}
