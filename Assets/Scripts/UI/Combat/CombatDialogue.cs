using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button defendButton;
    [SerializeField] private Button itemButton;
    [SerializeField] private Button escapeButton;

    private Button[] buttons;

    private void Awake()
    {
        dialogueText.text = string.Empty;
        buttons = new Button[] { attackButton, defendButton, itemButton, escapeButton };
        PlayerCombatController pcc = Player.Instance.GetComponent<PlayerCombatController>();
        attackButton.onClick.AddListener(pcc.ButtonAttack);
        defendButton.onClick.AddListener(pcc.ButtonDefend);
        itemButton.onClick.AddListener(pcc.ButtonItem);
        escapeButton.onClick.AddListener(pcc.ButtonEscape);
        HideButtons();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatStatusChange += CombatStatusChange;
        GameEventsManager.Instance.CombatEvents.onPlayerTurn += ShowButtons;
        GameEventsManager.Instance.CombatEvents.onEndTurn += HideButtons;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatStatusChange -= CombatStatusChange;
        GameEventsManager.Instance.CombatEvents.onPlayerTurn -= ShowButtons;
        GameEventsManager.Instance.CombatEvents.onEndTurn -= HideButtons;
    }

    private void ShowButtons()
    {
        // If the buttons are already showing, return
        if (buttons[0].gameObject.activeInHierarchy) { return; }

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void HideButtons()
    {
        // If the buttons are already hidden, return
        if (!buttons[0].gameObject.activeInHierarchy) { return; }

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void CombatStatusChange(string status)
    {
        dialogueText.text = status;
    }
}
