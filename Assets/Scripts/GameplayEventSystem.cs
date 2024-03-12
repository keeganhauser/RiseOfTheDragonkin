using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEventSystem : MonoBehaviour
{
    public static GameplayEventSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
