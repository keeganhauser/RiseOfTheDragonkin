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
        enemyObj.name = enemy.enemyName;

        return enemyObj;
    }
}
