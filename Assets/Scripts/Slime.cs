using ROTDK.EnemyType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    [SerializeField]
    private string enemyName = "Slime";
    public string Name => enemyName;

    [SerializeField]
    private EnemyType enemyType = EnemyType.Slime;
    public EnemyType EnemyType => enemyType;
}
