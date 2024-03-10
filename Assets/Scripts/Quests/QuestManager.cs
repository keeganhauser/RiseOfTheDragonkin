using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
        //DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.QuestEvents.onStartQuest   += StartQuest;
        GameEventsManager.Instance.QuestEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.Instance.QuestEvents.onFinishQuest  += FinishQuest;
        
        GameEventsManager.Instance.QuestEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.QuestEvents.onStartQuest   -= StartQuest;
        GameEventsManager.Instance.QuestEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.Instance.QuestEvents.onFinishQuest  -= FinishQuest;

        GameEventsManager.Instance.QuestEvents.onQuestStepStateChange -= QuestStepStateChange;

    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            // Initialize any loaded quest steps
            if (quest.State == QuestState.InProgress)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }

            // Broadcast the initial state of each quest
            GameEventsManager.Instance.QuestEvents.QuestStateChange(quest);
        }
    }

    private void Update()
    {
        // Loop through all quests
        foreach (Quest quest in questMap.Values)
        {
            // If quest requirements are met, switch it to CanStart
            if (quest.State == QuestState.RequirementsNotMet && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.Info.ID, QuestState.CanStart);
            }
        }
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.Info.ID, QuestState.InProgress);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        // Move on to next step
        quest.MoveToNextStep();

        // If there is a next step, instantiate it
        if (quest.CurrentStepExists())
            quest.InstantiateCurrentQuestStep(this.transform);

        // If there is no next step, quest is finished
        else
            ChangeQuestState(quest.Info.ID, QuestState.CanFinish);

    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.Info.ID, QuestState.Finished);
    }

    private void ClaimRewards(Quest quest)
    {
        // TODO: Add rewards
        Debug.Log($"Claimed {quest.Info.GoldReward} gold");
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.State = state;
        GameEventsManager.Instance.QuestEvents.QuestStateChange(quest);
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.State);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        // Check quest prereqs for requirements
        foreach (QuestInfo questInfo in quest.Info.QuestPrerequisites)
        {
            if (GetQuestById(questInfo.ID).State != QuestState.Finished)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfo[] allQuests = Resources.LoadAll<QuestInfo>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfo questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.ID))
            {
                Debug.LogWarning($"Duplicate ID found when creating quest map: {questInfo.ID}");
            }
            //idToQuestMap.Add(questInfo.ID, LoadQuest(questInfo));
            Debug.Log($"Found quest '{questInfo.ID}'");
            idToQuestMap.Add(questInfo.ID, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        questMap.TryGetValue(id, out Quest quest);
        if (quest == null)
            Debug.LogError($"ID not found in quest map: {id}");

        return quest;
    }

    private void OnApplicationQuit()
    {
        // TODO: Save game
        //foreach (Quest quest in questMap.Values)
        //{
        //}
    }

    //private void SaveQuest(Quest quest)
    //{
    //    try
    //    {
    //        QuestData questData = quest.GetQuestData();
    //        string serializedData = JsonUtility.ToJson(questData);
    //        PlayerPrefs.SetString(quest.Info.ID, serializedData);

    //        Debug.Log(serializedData);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError($"Error when saving: {e}");
    //    }
    //}

    //private Quest LoadQuest(QuestInfo questInfo)
    //{
    //    Quest quest = null;

    //    try
    //    {
    //        // Load quest from saved data
    //        if (PlayerPrefs.HasKey(questInfo.ID))
    //        {
    //            string serializedData = PlayerPrefs.GetString(questInfo.ID);
    //            QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
    //            quest = new Quest(questInfo, quest.State, questData.questStepIndex, questData.questStepStates);
    //        }

    //        // Otherwise initialize quest with default data
    //        else
    //        {
    //            quest = new Quest(questInfo);
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError($"Error when loading: {e}");
    //    }
    //    return quest;
    //}
}
