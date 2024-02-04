using ROTDK.EnemyType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROTDK.EnemyType
{
    public enum EnemyType
    {
        Slime,
        Other
    }
}

public interface IEnemy
{
    public string Name { get; }
    public int Health { get; set; }
    public EnemyType EnemyType { get; }
}
