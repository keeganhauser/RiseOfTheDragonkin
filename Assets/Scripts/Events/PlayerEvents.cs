using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        Debug.Log("Player movement disabled");
        onDisablePlayerMovement?.Invoke();
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        Debug.Log("Player movement enabled");
        onEnablePlayerMovement?.Invoke();
    }

    public event Action<int> onExperienceGained;
    public void ExperienceGained(int experience)
    {
        onExperienceGained?.Invoke(experience);
    }

    public event Action<int> onPlayerExperienceChange;
    public void PlayerExperienceChange(int experience)
    {
        onPlayerExperienceChange?.Invoke(experience);
    }

    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level)
    {
        onPlayerLevelChange?.Invoke(level);
    }

    // TODO: Enable/disable player interactions


    public event Action<int> onPlayerAttack;
    public void PlayerAttack(int damage)
    {
        onPlayerAttack?.Invoke(damage);
    }

    public event Action onPlayerDecideAttack;
    public void PlayerDecideAttack()
    {
        onPlayerDecideAttack?.Invoke();
    }

    public event Action onPlayerInitializeFinish;
    public void PlayerInitializeFinish()
    {
        onPlayerInitializeFinish?.Invoke();
    }

    public event Action onPlayerWasHit;
    public void PlayerWasHit()
    {
        onPlayerWasHit?.Invoke();
    }

    public event Action onPlayerDeath;
    public void PlayerDeath()
    {
        onPlayerDeath?.Invoke();
    }

    public event Action onPlayerRevive;
    public void PlayerRevive()
    {
        onPlayerRevive?.Invoke();
    }

    public event Action onPlayerHealthChange;
    public void PlayerHealthChange()
    {
        onPlayerHealthChange?.Invoke();
    }

    public event Action onPlayerManaChange;
    public void PlayerManaChange()
    {
        onPlayerManaChange?.Invoke();
    }

    public event Action onPlayerExpChange;
    public void PlayerExpChange()
    {
        onPlayerExpChange?.Invoke();
    }
}
