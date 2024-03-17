using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : SingletonMonoBehavior<CustomSceneManager>
{
    private bool isFading;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] CanvasGroup loadingScreenPanel;
    public SceneName startingScene;

    private IEnumerator Start()
    {
        // Set initial alpha to start with black screen
        loadingScreenPanel.GetComponent<Image>().color = Color.black;
        loadingScreenPanel.alpha = 1f;

        yield return StartCoroutine(LoadSceneAndSetActive(startingScene.ToString()));

        if (SaveLoadManager.Instance == null)
            Debug.Log("SaveLoadManager is null");
        SaveLoadManager.Instance.RestoreCurrentSceneData();

        StartCoroutine(Fade(0f));
    }

    public void FadeAndLoadScene(string sceneName, Vector3 spawnPosition)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName, spawnPosition));
        }
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName, Vector3 spawnPosition)
    {
        // Reset loading screen text
        loadingText.text = "Loading... 0%";

        // Fade screen to black
        GameEventsManager.Instance.SceneEvents.SceneFadeOut();
        yield return StartCoroutine(Fade(1f));


        // Save scene data
        SaveLoadManager.Instance.StoreCurrentSceneData();

        // Set player position
        Player.Instance.transform.position = spawnPosition;
        Debug.Log($"Spawn position: {spawnPosition}");

        // Unload current scene
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        // Load next scene
        GameEventsManager.Instance.SceneEvents.SceneSwitchStart();
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        GameEventsManager.Instance.SceneEvents.SceneSwitchEnd();

        // Load new scene data
        SaveLoadManager.Instance.RestoreCurrentSceneData();

        // Chill for a sec
        yield return new WaitForSeconds(1f);

        // Fade screen back in
        yield return StartCoroutine(Fade(0f));
        
        GameEventsManager.Instance.SceneEvents.SceneFadeIn();
    }

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        loadingScreenPanel.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(loadingScreenPanel.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(loadingScreenPanel.alpha, finalAlpha))
        {
            loadingScreenPanel.alpha = Mathf.MoveTowards(
                loadingScreenPanel.alpha,
                finalAlpha,
                fadeSpeed * Time.deltaTime);

            yield return null;
        }

        isFading = false;
        loadingScreenPanel.blocksRaycasts = false;
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        // Load the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingText.text = $"Loading... {progressValue * 100:F2}%";
            yield return null;
        }

        // Get the scene that was just loaded
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        // Set newly loaded scene as the active scene
        SceneManager.SetActiveScene(newlyLoadedScene);
    }
}
