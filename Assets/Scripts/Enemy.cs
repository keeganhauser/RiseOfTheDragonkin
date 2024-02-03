using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int hp = 20;

    [SerializeField]
    private string _name = "Enemy";

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
