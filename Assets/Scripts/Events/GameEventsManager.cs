using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public NPCEvents        NPCEvents;
    public QuestEvents      QuestEvents;
    public PlayerEvents     PlayerEvents;
    public InputEvents      InputEvents;
    public InventoryEvents  InventoryEvents;
    public CombatEvents     CombatEvents;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Found more than one Game Events Manager in the scene.");

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize events
        NPCEvents       = new NPCEvents();
        QuestEvents     = new QuestEvents();
        PlayerEvents    = new PlayerEvents();
        InputEvents     = new InputEvents();
        InventoryEvents = new InventoryEvents();
        CombatEvents    = new CombatEvents();
    }
}
