using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : SingletonMonoBehavior<GameEventsManager>
{
    public NPCEvents        NPCEvents;
    public QuestEvents      QuestEvents;
    public PlayerEvents     PlayerEvents;
    public InputEvents      InputEvents;
    public InventoryEvents  InventoryEvents;
    public CombatEvents     CombatEvents;
    public CraftingEvents   CraftingEvents;

    protected override void Awake()
    {
        base.Awake();

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
