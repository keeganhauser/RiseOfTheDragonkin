using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Static variables
    public static Player Instance;

    // Serialized variables
    [SerializeField]
    public string Name { get; private set; } = "DefaultName";

    // Private methods
    private void Awake()
    {
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadSceneAsync("CombatScene");
    }
}
