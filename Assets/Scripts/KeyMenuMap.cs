using System;
using UnityEngine;

[Serializable]
public class KeyMenuMap : ScriptableObject
{
    [SerializeField]
    private Menu menu;

    [SerializeField]
    private KeyCode keyCode;
}
