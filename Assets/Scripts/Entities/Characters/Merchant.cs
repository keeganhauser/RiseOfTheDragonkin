using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : NPC, IInteractable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    private QuestPoint questPoint;

    private void Awake()
    {
        questPoint = GetComponent<QuestPoint>();
    }

    public override void Interact()
    {
        GameEventsManager.Instance.NPCEvents.TriggerInteract(this);
        questPoint?.StartQuest();

        if (dialogueController != null) 
        { 
            Talk(dialogueText);
        }
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }
}
