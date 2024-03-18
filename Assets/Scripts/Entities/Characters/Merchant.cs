using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : NPC, IInteractable
{
    [SerializeField] private DialogueText dialogueText;
    private DialogueController dialogueController;
    private QuestPoint[] questPoint;

    private void Awake()
    {
        questPoint = GetComponents<QuestPoint>();
        dialogueController = FindFirstObjectByType<DialogueController>();
    }

    public override void Interact()
    {
        if (!IsWithinInteractDistance()) return;

        GameEventsManager.Instance.NPCEvents.TriggerInteract(this);

        foreach (QuestPoint p in questPoint)
        {
            p?.StartQuest();
        }


        if (dialogueController != null) 
        {
            Debug.Log("Merchant talk");
            Talk(dialogueText);
        }
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }
}
