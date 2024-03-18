using System;
using UnityEngine;
public class QuestEvents
{
    public event Action<string> onStartQuest;
    public void StartQuest(string id)
    {
        Debug.Log($"Starting quest {id}");
        onStartQuest?.Invoke(id);
    }

    public event Action<string> onAdvanceQuest;
    public void AdvanceQuest(string id)
    {
        onAdvanceQuest?.Invoke(id);
    }

    public event Action<string> onFinishQuest;
    public void FinishQuest(string id)
    {
        onFinishQuest?.Invoke(id);
    }

    public event Action<Quest> onQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        Debug.Log($"{quest.Info.DisplayName} has been set to {quest.State}");
        onQuestStateChange?.Invoke(quest);
    }

    public event Action<string, int, QuestStepState> onQuestStepStateChange;
    public void QuestStepStateChange(string id, int stepIndex,  QuestStepState stepState)
    {
        onQuestStepStateChange?.Invoke(id, stepIndex, stepState);
    }

    public event Action onGetAllQuestStates;
    public void GetAllQuestStates()
    {
        onGetAllQuestStates?.Invoke();
    }
}
