using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    // Static variables
    public static Player Instance;

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
            DontDestroyOnLoad(gameObject);
        }
        else if (this != Instance)
        {
            Destroy(this);
        }
    }
}
