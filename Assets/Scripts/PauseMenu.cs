using MariaDBLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    MariaDB db;

    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private Button loadButton;

    void Start()
    {
        db = FindObjectOfType<MariaDB>();
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
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
