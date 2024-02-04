using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int hp = 20;

    [SerializeField]
    public string _name = "Enemy";

    [SerializeField]
    public EnemyType Type { get; private set; }

    public enum EnemyType
    {
        Slime
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{_name} took {damage} damage!");

        if ( hp <= 0 )
        {
            Die();
        }
    } 

    private void Die()
    {
        Debug.Log($"{_name} has been defeated!");
        Destroy(gameObject);
    }
}
