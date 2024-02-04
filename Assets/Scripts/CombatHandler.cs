using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    private Vector2 playerPosition = new Vector2(-3.5f, -0.5f);
    private Vector2 enemyPosition = new Vector2(4.5f, -0.5f);

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void EnterCombat(Collider2D enemyCol)
    {
        Enemy enemy = enemyCol.GetComponent<Enemy>();
        Debug.Log($"{Player.Instance.Name} has entered combat with {enemy._name}!");
        Player.Instance.transform.position = playerPosition;
        Player.Instance.ResetPosition();
        enemy.transform.position = enemyPosition;
    }
}
