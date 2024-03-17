using System;

public class SceneEvents
{
    public event Action onSceneFadeOut;
    public void SceneFadeOut()
    {
        onSceneFadeOut?.Invoke();
    }

    public event Action onSceneFadeIn;
    public void SceneFadeIn()
    {
        onSceneFadeIn?.Invoke();
    }

    public event Action onSceneSwitchStart;
    public void SceneSwitchStart()
    {
        onSceneSwitchStart?.Invoke();
    }

    public event Action onSceneSwitchEnd;
    public void SceneSwitchEnd()
    {
        onSceneSwitchEnd?.Invoke();
    }

}
