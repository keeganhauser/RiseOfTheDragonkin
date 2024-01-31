using MariaDBLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private MariaDB db;
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
        db = FindObjectOfType<MariaDB>();
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
        isPaused = !isPaused;

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
        Player player = FindObjectOfType<Player>();
        Vector3 pos = player.GetComponent<Transform>().position;
        DateTime date = DateTime.Now;

        db.Insert(player.Name, pos.ToString("F6"), date);
        Debug.Log($"Game saved at {date}");
    }

    public void LoadGame()
    {
        string readQ = "SELECT * FROM save_data";
        string retrieveLast = "SELECT * FROM save_data ORDER BY DATE DESC LIMIT 1";
        Debug.Log(db.Read(retrieveLast));
    }
}
