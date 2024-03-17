using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    private static readonly Color NOT_STARTED_COLOR = new Color(220 / 255f, 0f, 0f);
    private static readonly Color IN_PROGRESS_COLOR = new Color(219 / 255f, 190 / 255f, 44 / 255f);
    private static readonly Color FINISHED_COLOR = new Color(19 / 255f, 194 / 255f, 40 / 255f);

    public Button Button { get; private set; }
    private TextMeshProUGUI buttonText;
    private UnityAction onSelectAction;

    public void Initialize(string displayName, UnityAction selectAction)
    {
        this.Button = GetComponent<Button>();
        this.buttonText = GetComponentInChildren<TextMeshProUGUI>();
        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction.Invoke();
    }

    public void SetState(QuestState state)
    {
        switch (state)
        {
            case QuestState.RequirementsNotMet:
            case QuestState.CanStart:
                buttonText.color = NOT_STARTED_COLOR;
                break;
            case QuestState.InProgress:
            case QuestState.CanFinish:
                buttonText.color = IN_PROGRESS_COLOR;
                break;
            case QuestState.Finished:
                buttonText.color = FINISHED_COLOR;
                break;
            default:
                break;
        }
    }
}
