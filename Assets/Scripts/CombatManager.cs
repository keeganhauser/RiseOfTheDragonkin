using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    private Transform playerTransform;
    public Enemy enemy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }

    public void StartEncounter(Enemy enemy)
    {
        // Capture enemy to enter combat with
        this.enemy = enemy;

        // Store player's position outside of combat
        playerTransform = Player.Instance.transform;

        // Load combat scene
        SceneManager.LoadScene("CombatScene");
    }

    public void EndEncounter()
    {
        // Unload combat scene
        SceneManager.UnloadSceneAsync("CombatScene");

        // Move player back
        Player.Instance.transform.position = playerTransform.position;
    }
}
