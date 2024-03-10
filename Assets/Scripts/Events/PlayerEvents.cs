using System;

public class PlayerEvents
{
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        onDisablePlayerMovement?.Invoke();
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
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
}
