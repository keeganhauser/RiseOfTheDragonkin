using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image expBar;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI levelText;

    private PlayerStats stats;

    private void Awake()
    {
        ShowUI();
    }

    private void GetStats()
    {
        stats = Player.Instance.Stats;
        UpdateUI();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.PlayerEvents.onPlayerInitializeFinish += GetStats;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.PlayerEvents.onPlayerInitializeFinish -= GetStats;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.fillAmount = Mathf.Lerp(
            healthBar.fillAmount,
            stats.Health / stats.MaxHealth,
            10 * Time.deltaTime);

        manaBar.fillAmount = Mathf.Lerp(
            manaBar.fillAmount,
            stats.Mana / stats.MaxMana,
            10 * Time.deltaTime);

        expBar.fillAmount = Mathf.Lerp(
            expBar.fillAmount,
            stats.CurrentExp / stats.NextLevelExp,
            10 * Time.deltaTime);

        levelText.text = $"Level {stats.Level}";
        healthText.text = $"{stats.Health} / {stats.MaxHealth}";
        manaText.text = $"{stats.Mana} / {stats.MaxMana}";
        expText.text = $"{stats.CurrentExp} / {stats.NextLevelExp}";
    }

    private void ShowUI()
    {
        if (!contentParent.activeInHierarchy)
            contentParent.SetActive(true);
    }

    private void HideUI()
    {
        if (contentParent.activeInHierarchy)
            contentParent.SetActive(false);
    }
}
