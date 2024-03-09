using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [Header("Quest Log Button")]
    [SerializeField] private GameObject questLogButtonPrefab;

    private Dictionary<string, QuestLogButton> idToButtonMap = new Dictionary<string, QuestLogButton>();

    public QuestLogButton CreateButtonIfNotExists(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = null;

        if (!idToButtonMap.ContainsKey(quest.Info.ID))
        {
            questLogButton = InstantiateQuestLogButton(quest, selectAction);
        }
        else
        {
            questLogButton = idToButtonMap[quest.Info.ID];
        }
        return questLogButton;
    }

    private QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
    {
        // Create button
        QuestLogButton questLogButton = Instantiate(
            questLogButtonPrefab,
            contentParent.transform)
            .GetComponent<QuestLogButton>();

        // Set name in scene
        questLogButton.gameObject.name = $"{quest.Info.ID}_button";

        // Init button
        questLogButton.Initialize(quest.Info.DisplayName, selectAction);

        // Add to map to keep track of button
        idToButtonMap.Add(quest.Info.ID, questLogButton);

        return questLogButton;
    }
    
}
