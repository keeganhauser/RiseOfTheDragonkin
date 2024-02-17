using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyMenuMap : ScriptableObject
{
    [SerializeField]
    private Menu menu;

    [SerializeField]
    private KeyCode keyCode;
}
