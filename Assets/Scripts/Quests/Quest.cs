using UnityEngine;

public class Quest
{
    public QuestInfo questInfo;
    public QuestState state;
    private int currentQuestStepIndex;

    public Quest(QuestInfo questInfo)
    {
        this.questInfo = questInfo;
        this.state = QuestState.RequirementsNotMet;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < questInfo.questStepPrefabs.Length;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if (questStepPrefab != null )
        {
            Object.Instantiate(questStepPrefab, parentTransform);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = questInfo.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex is out of range.");
        }

        return questStepPrefab;
    }
}