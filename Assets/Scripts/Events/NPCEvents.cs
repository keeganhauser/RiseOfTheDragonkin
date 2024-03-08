using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEvents
{
    public event Action<NPC> onNPCInteract;
    public void TriggerInteract(NPC npc)
    {
        Debug.Log($"Interacted with {npc.NPCName}");
        onNPCInteract?.Invoke(npc);
    }
}
