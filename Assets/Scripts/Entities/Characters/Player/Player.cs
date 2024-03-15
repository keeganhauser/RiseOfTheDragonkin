using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : SingletonMonoBehavior<Player>
{
    [SerializeField] public string Name;
}
