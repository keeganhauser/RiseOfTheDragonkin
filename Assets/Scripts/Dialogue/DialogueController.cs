using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private float typeSpeed = 10f;

    private Queue<string> paragraphs = new Queue<string>();
    private bool conversationEnded;
    private string paragraph;
    private Coroutine typeDialogueCoroutine;
    private bool isTyping;
    private const float MAX_TYPE_TIME = 1f;

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        if (paragraphs.Count == 0)
        {
            if (!conversationEnded)
            {
                // Start conversation
                StartConversation(dialogueText);
            }
            else if (conversationEnded && !isTyping)
            {
                // End conversation
                EndConversation();
                return;
            }
        }

        // If there is something in the queue:
        if (!isTyping)
        {
            paragraph = paragraphs.Dequeue();
            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(paragraph));
        }
        else
        {
            FinishParagraphEarly();
        }

        // End conversation
        if (paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogueText)
    {
        // Activate gameObject
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }

        // Update speaker name
        NPCNameText.text = dialogueText.speakerName;

        // Add paragraphs to queue
        foreach (string paragraph in dialogueText.paragraphs)
        {
            paragraphs.Enqueue(paragraph);
        }
    }

    private void EndConversation()
    {
        // Clear queue
        paragraphs.Clear();

        // Return bool to false so we can talk to NPC again
        conversationEnded = false;

        // Deactivate gameObject
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeDialogueText(string paragraph)
    {
        isTyping = true;
        int maxVisibleChars = 0;

        NPCDialogueText.text = paragraph;
        NPCDialogueText.maxVisibleCharacters = maxVisibleChars;

        foreach (char  character in paragraph)
        {
            maxVisibleChars++;
            NPCDialogueText.maxVisibleCharacters = maxVisibleChars;
            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        // Stop coroutine
        StopCoroutine(typeDialogueCoroutine);

        // Finish displaying text
        NPCDialogueText.maxVisibleCharacters = paragraph.Length;

        // Update isTyping
        isTyping = false;
    }
}
