using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public NPCEvents npcEvents;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Found more than one Game Events Manager in the scene.");

        Instance = this;

        // Initialize events
        npcEvents = new NPCEvents();
    }
}
