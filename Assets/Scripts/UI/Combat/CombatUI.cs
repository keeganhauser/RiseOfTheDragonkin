using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private Image dialoguePanel;

    private Image combatUI;

    private void Awake()
    {
        combatUI = GetComponent<Image>();
        combatUI.raycastTarget = false;
        HideUI();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPreInitialization += ShowUI;
        GameEventsManager.Instance.CombatEvents.onCombatEnd += HideUI;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPreInitialization -= ShowUI;
        GameEventsManager.Instance.CombatEvents.onCombatEnd -= HideUI;
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        dialoguePanel.raycastTarget = true;
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        dialoguePanel.raycastTarget = false;
    }
}
