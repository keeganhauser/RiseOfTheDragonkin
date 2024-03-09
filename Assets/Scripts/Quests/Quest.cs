using System.Text;
using UnityEngine;

public class Quest
{
    public QuestInfo Info;
    public QuestState State;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;

    public Quest(QuestInfo questInfo)
    {
        this.Info = questInfo;
        this.State = QuestState.RequirementsNotMet;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[Info.QuestStepPrefabs.Length];

        for (int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }

    public Quest(QuestInfo questInfo, QuestState questState,  int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.Info = questInfo;
        this.State = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        if (this.questStepStates.Length != this.Info.QuestStepPrefabs.Length)
        {
            Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                + "of different lengths. This indicates something changed "
                + "with the QuestInfo and the saved data is now out of sync. "
                + "Reset your data - as this might cause issues. QuestId: " + this.Info.ID);
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < Info.QuestStepPrefabs.Length;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitializeQuestStep(
                Info.ID, 
                currentQuestStepIndex,
                questStepStates[currentQuestStepIndex].State);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = Info.QuestStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex is out of range.");
        }

        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].State = questStepState.State;
            questStepStates[stepIndex].Status = questStepState.Status;
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(State, currentQuestStepIndex, questStepStates);
    }

    public string GetFullStatusText()
    {
        string fullStatus = string.Empty;

        if (State == QuestState.RequirementsNotMet)
        {
            fullStatus = "Requirements are not yet met to start this quest.";
        }
        else if (State == QuestState.CanStart)
        {
            fullStatus = "This quest can be started!";
        }
        else
        {
            // Display all previous quests with strikethroughs
            for (int i = 0; i < currentQuestStepIndex; i++)
            {
                fullStatus += $"<s> {questStepStates[i].Status} </s>\n";
            }

            // Display current step, if it exists
            if (CurrentStepExists())
            {
                fullStatus += questStepStates[currentQuestStepIndex].Status;
            }

            // When the quest is completed or turned in
            if (State == QuestState.CanFinish)
            {
                fullStatus += "This quest is ready to be turned in.";
            }
            else if (State == QuestState.Finished)
            {
                fullStatus += "Quest completed!";
            }
        }

        return fullStatus;
    }
}