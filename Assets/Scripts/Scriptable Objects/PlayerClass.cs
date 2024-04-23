using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "New Player class", menuName = "Player Class")]
public class PlayerClass : ScriptableObject
{
    public string defaultPlayerName;
    public SpriteLibraryAsset spriteLibrary;
    // TODO: Add default items
}
