using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    private Image combatUI;

    private void Awake()
    {
        combatUI = GetComponent<Image>();
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
        combatUI.raycastTarget = true;
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        combatUI.raycastTarget = false;
    }
}
