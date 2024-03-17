using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
    [SerializeField] private Button showQuestLogButton;
    [SerializeField] private Button darkBackgroundButton;


    private Button firstSelectedButton;

    private void Awake()
    {
        // Setup the button's onClick to trigger the onInventoryTogglePressed event
        showQuestLogButton.onClick.AddListener(() => GameEventsManager.Instance.InputEvents.QuestLogTogglePressed());
        darkBackgroundButton.onClick.AddListener(() => GameEventsManager.Instance.InputEvents.QuestLogTogglePressed());
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.QuestEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.Instance.InputEvents.onQuestLogTogglePressed += ToggleUI;
        GameEventsManager.Instance.InputEvents.onInventoryTogglePressed += ToggleButton;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.QuestEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.Instance.InputEvents.onQuestLogTogglePressed -= ToggleUI;
        GameEventsManager.Instance.InputEvents.onInventoryTogglePressed -= ToggleButton;
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

    private void ToggleUI()
    {
        if (contentParent.activeInHierarchy)
            HideUI();
        else
            ShowUI();
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();

        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
        ToggleButton();
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
        EventSystem.current.SetSelectedGameObject(null);
        ToggleButton();
    }

    private void ToggleButton()
    {
        showQuestLogButton.gameObject.SetActive(!showQuestLogButton.gameObject.activeInHierarchy);
    }
}
