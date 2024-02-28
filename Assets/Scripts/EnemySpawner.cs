using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static GameObject SpawnEnemy(Enemy enemy)
    {
        return SpawnEnemy(enemy, Vector3.zero, Quaternion.identity, null);
    }

    public static GameObject SpawnEnemy(
        Enemy enemy, 
        Vector3 position, 
        Quaternion rotation, 
        Transform parent)
    {
        GameObject enemyObj = Instantiate(enemy.enemyPrefab, position, rotation, parent);

        SpriteRenderer spriteRenderer = enemyObj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = enemy.sprite;
            spriteRenderer.material = enemy.material;
        }

        return enemyObj;
    }
}
