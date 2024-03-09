using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class NPCApproachQuestStep : QuestStep
{
    [Header("Config")]
    [SerializeField] private string NPCName;
    [SerializeField] private string location;
    private void Start()
    {
        string status = $"Find {NPCName} in {location}.";
        ChangeState(string.Empty, status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state is needed for this quest
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Player.Instance.tag)
        {
            string status = $"You found {NPCName}.";
            ChangeState(string.Empty, status);
            FinishQuestStep();
        }
    }
}
