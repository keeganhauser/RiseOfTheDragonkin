using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;

    private void Awake()
    {
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
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
    }
}
