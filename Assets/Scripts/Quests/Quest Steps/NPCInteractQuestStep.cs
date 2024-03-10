using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractQuestStep : QuestStep
{
    [Header("Config")]
    [SerializeField] private string location;
    [SerializeField] private GameObject NPCPrefab;
    
    private NPC trackedNPC;
    
    private void Awake()
    {
        
        trackedNPC = NPCPrefab.GetComponent<NPC>();
        if (trackedNPC == null)
        {
            Debug.LogError("Could not locate NPC component on NPC Prefab");
        }
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.NPCEvents.onNPCInteract += Interacted;
    }

    private void Start()
    {
        string status = $"Talk to {trackedNPC.Name} in {location}.";
        ChangeState(string.Empty, status);
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.NPCEvents.onNPCInteract -= Interacted;
    }

    private void Interacted(NPC npc)
    {
        if (npc.Name == trackedNPC.Name)
        {
            string status = $"You talked to {trackedNPC.Name}.";
            ChangeState(string.Empty, status);
            FinishQuestStep();
        }
    }

    //private void UpdateState()
    //{
    //    string state = $"{trackedNPC.NPCName},{interacted}";
    //    ChangeState(state);
    //}

    protected override void SetQuestStepState(string state)
    {
        //if (!bool.TryParse(state.Split(',')[1], out interacted))
        //    Debug.LogError("Error parsing interacted bool");
        
        // TODO: Set quest step state
    }
}
