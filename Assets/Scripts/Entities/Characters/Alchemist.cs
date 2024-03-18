using UnityEngine;

public class Alchemist : NPC, ITalkable
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

        // If player is within interaction distance:
        GameEventsManager.Instance.NPCEvents.TriggerInteract(this);
        foreach (QuestPoint p in questPoint)
        {
            p?.StartQuest();
        }

        Debug.Log("Merchant talk");
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);    
    }
}
