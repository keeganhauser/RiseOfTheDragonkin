using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "New Player class", menuName = "Player Class")]
public class PlayerClass : ScriptableObject
{
    public string defaultPlayerName;
    public int damage;
    public int health;
    public int mana;
    public int speed;
    public SpriteLibraryAsset spriteLibrary;
}
