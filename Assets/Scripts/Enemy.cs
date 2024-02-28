using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable object/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("General")]
    public string enemyName;
    public string description;
    public GameObject enemyPrefab;

    [Header("Visual")]
    public Sprite sprite;
    public Material material;
}
