using MariaDBLib;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    private Image image;

    [SerializeField]
    private TMP_Text pauseText;

    [SerializeField]
    [Range(0f, 1f)]
    private float pauseTextSpeed;

    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private Button loadButton;

    void Start()
    {
        isPaused = false;
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
        image = GetComponent<Image>();
        image.enabled = isPaused;

        // Activate/Deactivate all children
        foreach (GameObject child in GetChildren())
        {
            child.SetActive(isPaused);
        }
    }

    void Update()
    {
        // Pause game when ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (isPaused)
        {
            UpdatePauseText();
        }
    }

    private IEnumerable<GameObject> GetChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            yield return transform.GetChild(i).gameObject;
        }
    }

    private void UpdatePauseText()
    {
        pauseText.color = Color.HSVToRGB(
            Mathf.PingPong(Time.time * pauseTextSpeed, 1),
            1, 1);
    }

    private void TogglePauseMenu()
    {
        //isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;

        // Enable/disable panel's background image and all children
        image.enabled = isPaused;
        foreach (GameObject child in GetChildren())
        {
            child.SetActive(isPaused);
        }
    }

    public void ResumeGame()
    {
        TogglePauseMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        SaveLoadManager.Instance.Save();
    }

    public void LoadGame()
    {
        SaveLoadManager.Instance.Load();
        ResumeGame();
    }
}
