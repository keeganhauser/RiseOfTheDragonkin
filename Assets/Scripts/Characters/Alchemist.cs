using UnityEngine;

public class Alchemist : NPC, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;

    public override void Interact()
    {
        Talk(dialogueText);
        InteractionEvent.Invoke();
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);    
    }
}
