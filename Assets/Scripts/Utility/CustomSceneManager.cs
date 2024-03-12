using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance;

    private bool isFading;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] CanvasGroup loadingScreenPanel;
    public string startingScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private IEnumerator Start()
    {
        // Set initial alpha to start with black screen
        loadingScreenPanel.GetComponent<Image>().color = Color.black;
        loadingScreenPanel.alpha = 1f;

        yield return StartCoroutine(LoadSceneAndSetActive(startingScene));

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
        // Fade screen to black
        yield return StartCoroutine(Fade(1f));

        // Set player position
        Player.Instance.gameObject.transform.position = spawnPosition;

        // Unload current scene
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        // Load next scene
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        // Fade screen back in
        yield return StartCoroutine(Fade(0f));
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
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Get the scene that was just loaded
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        // Set newly loaded scene as the active scene
        SceneManager.SetActiveScene(newlyLoadedScene);
    }
}
