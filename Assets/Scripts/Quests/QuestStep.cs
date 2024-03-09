using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    private int stepIndex;

    public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
        if (!string.IsNullOrEmpty(questStepState))
        {
            SetQuestStepState(questStepState);
        }
    }

    
    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            GameEventsManager.Instance.QuestEvents.AdvanceQuest(questId);
            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState, string newStatus)
    {
        GameEventsManager.Instance.QuestEvents.QuestStepStateChange(
            questId,
            stepIndex,
            new QuestStepState(newState, newStatus));
    }

    protected abstract void SetQuestStepState(string state);
}
