using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private PlayerStats stats;

    private void Start()
    {
        stats = Player.Instance.Stats;
    }

    public void AddExp(float amount)
    {
        stats.CurrentExp += amount;
        while (stats.CurrentExp >= stats.NextLevelExp)
        {
            stats.CurrentExp -= stats.NextLevelExp;
            NextLevel();
        }
        GameEventsManager.Instance.PlayerEvents.PlayerExpChange();
    }

    private void NextLevel()
    {
        stats.Level++;
        float currentExpRequired = stats.NextLevelExp;
        float newNextLevelExp = Mathf.Round(
            currentExpRequired + stats.NextLevelExp * (stats.ExpMultiplier / 100f));
        stats.NextLevelExp = newNextLevelExp;
    }
}
