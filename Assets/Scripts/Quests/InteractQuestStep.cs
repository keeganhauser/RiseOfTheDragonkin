using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractQuestStep : QuestStep
{
    private NPC npc;

    private void OnEnable()
    {
        GameEventsManager.Instance.npcEvents.onNPCInteract += Interacted;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.npcEvents.onNPCInteract -= Interacted;
    }

    private void Interacted(NPC npc)
    {
        FinishQuestStep();
    }
}
