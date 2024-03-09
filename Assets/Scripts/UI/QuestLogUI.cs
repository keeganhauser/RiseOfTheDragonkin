using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private QuestLogScrollingList scrollingList;
    [SerializeField] private TextMeshProUGUI questDisplayNameText;
    [SerializeField] private TextMeshProUGUI questStatusText;
    [SerializeField] private TextMeshProUGUI goldRewardsText;
    [SerializeField] private TextMeshProUGUI experienceRewardsText;
    [SerializeField] private TextMeshProUGUI itemRewardsText;
    [SerializeField] private TextMeshProUGUI levelRequirementsText;
    [SerializeField] private TextMeshProUGUI questRequirementsText;

    private Button firstSelectedButton;

    private void OnEnable()
    {
        GameEventsManager.Instance.QuestEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.QuestEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        // Add the button to the scrolling list if not already added
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () =>
        {
            SetQuestLogInfo(quest);
        });

        // Set questLogButton to first selected button if select button is null
        if (firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.Button;
            firstSelectedButton.Select();
        }

        // Set button text color based on quest state
        questLogButton.SetState(quest.State);
    }

    private void SetQuestLogInfo(Quest quest)
    {
        // Quest name
        questDisplayNameText.text = quest.Info.DisplayName;

        // Status
        questStatusText.text = quest.GetFullStatusText();

        // Requirements
        levelRequirementsText.text = $"Level {quest.Info.LevelRequirement}";

        questRequirementsText.text = string.Empty;
        foreach (QuestInfo prerequisiteQuestInfo in quest.Info.QuestPrerequisites)
        {
            questRequirementsText.text += $"{prerequisiteQuestInfo.DisplayName}\n";
        }

        // Rewards
        goldRewardsText.text = $"{quest.Info.GoldReward} Gold";
        experienceRewardsText.text = $"{quest.Info.ExperienceReward} XP";
        
        // TODO: Implement item rewards quest info
        itemRewardsText.text = "";


    }
}
