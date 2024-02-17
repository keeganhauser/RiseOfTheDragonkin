using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestStatus
{
    public Quest questData;

    public Dictionary<int, Quest.Status> objectiveStatuses;

    public QuestStatus(Quest questData)
    {
        this.questData = questData;

        objectiveStatuses = new Dictionary<int, Quest.Status>();

        for (int i = 0; i < questData.objectives.Count; i++)
        {
            objectiveStatuses[i] = questData.objectives[i].initialStatus;
        }
    }

    public Quest.Status questStatus
    {
        get
        {
            for (int i = 0; i < questData.objectives.Count; i++)
            {
                Quest.Objective objectiveData = questData.objectives[i];

                if (objectiveData.optional)
                    continue;

                Quest.Status objectiveStatus = objectiveStatuses[i];

                if (objectiveStatus == Quest.Status.Failed)
                {
                    return Quest.Status.Failed;
                }
                else if (objectiveStatus != Quest.Status.Complete)
                {
                    return Quest.Status.NotComplete;
                }
            }
            return Quest.Status.Complete;
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < questData.objectives.Count; i++)
        {
            Quest.Objective objectiveData = questData.objectives[i];
            Quest.Status objectiveStatus = objectiveStatuses[i];

            if (!objectiveData.visible && objectiveStatus == Quest.Status.NotComplete) continue;

            if (objectiveData.optional)
            {
                stringBuilder.AppendLine($"{objectiveData.name} (Optional) - {objectiveStatus}");
            }
            else
            {
                stringBuilder.AppendLine($"{objectiveData.name} - {objectiveStatus}");
            }
        }

        stringBuilder.AppendLine();
        stringBuilder.AppendLine($"Status: {this.questStatus}");

        return stringBuilder.ToString();
    }
}

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private Quest startingQuest;

    [SerializeField]
    private TMP_Text objectiveSummary;

    private QuestStatus activeQuest;

    void Start()
    {
        if (startingQuest != null)
            StartQuest(startingQuest);
    }

    public void StartQuest(Quest quest)
    {
        activeQuest = new QuestStatus(quest);
        UpdateObjectiveSummaryText();
        Debug.Log($"Started quest {activeQuest.questData.name}");
    }

    private void UpdateObjectiveSummaryText() 
    { 
        objectiveSummary.text = (activeQuest == null) ? "No active quest" : activeQuest.ToString();
    }

    public void UpdateObjectiveStatus(Quest quest, int objectiveNumber, Quest.Status status)
    {
        if (activeQuest == null)
        {
            Debug.LogError($"Tried to set an objective status, but no quest is active");
            return;
        }

        if (activeQuest.questData != quest)
        {
            Debug.LogWarning($"Tried to set an objective status for quest {quest.questName}, but this is not the active quest.");
            return;
        }

        activeQuest.objectiveStatuses[objectiveNumber] = status;
        UpdateObjectiveSummaryText();
    }
}
