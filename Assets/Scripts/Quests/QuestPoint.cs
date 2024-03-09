using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfo questInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private string questId;
    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.ID;
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.QuestEvents.onQuestStateChange += QuestStateChanged;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.QuestEvents.onQuestStateChange -= QuestStateChanged;
    }

    public void StartQuest()
    {
        if ((currentQuestState == QuestState.CanStart) && startPoint)
        {
            GameEventsManager.Instance.QuestEvents.StartQuest(questId);
        }
        else if ((currentQuestState == QuestState.CanFinish) && finishPoint)
        {
            GameEventsManager.Instance.QuestEvents.FinishQuest(questId);
        }
    }

    private void QuestStateChanged(Quest quest)
    {
        if (quest.Info.ID == questId)
        {
            currentQuestState = quest.State;
            Debug.Log($"Quest '{questId}' updated to state {currentQuestState}");
        }
    }
}
