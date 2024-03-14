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
    public CraftingEvents   CraftingEvents;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize events
        NPCEvents       = new NPCEvents();
        QuestEvents     = new QuestEvents();
        PlayerEvents    = new PlayerEvents();
        InputEvents     = new InputEvents();
        InventoryEvents = new InventoryEvents();
        CombatEvents    = new CombatEvents();
        CraftingEvents  = new CraftingEvents();
    }
}
