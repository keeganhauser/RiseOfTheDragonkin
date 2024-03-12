using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCanvas : MonoBehaviour
{
    public static GameplayCanvas Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
