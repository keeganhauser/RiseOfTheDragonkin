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

    private bool inCombat = false;
    private CombatHandler combatHandler;
    private bool isDefending = false;

    public enum EnemyType
    {
        Slime
    }

    void Start()
    {

    }

    void Update()
    {
        // If in combat and is enemy's turn...
        if (inCombat && combatHandler.CurrentTurn == CombatHandler.Turn.Enemy)
        {
            // Make a move
            float rand = Random.Range(0, 1);
            if (rand < 0.8f)
            {
                Attack();
                Debug.Log("Enemy has attacked!");
            }
            else
            {
                Defend();
                Debug.Log("Enemy has defended!");
            }

            combatHandler.EndTurn();
        }
    }

    private void Attack()
    {

    }

    private void Defend()
    {
        isDefending = true;
    }

    public void InitiateCombat(CombatHandler handler)
    {
        inCombat = true;
        combatHandler = handler;
    }

    public void TakeDamage(int damage)
    {
        if (isDefending)
        {
            damage /= 2;
            isDefending = false;
        }
        hp -= damage;
        Debug.Log($"{_name} took {damage} damage!");

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{_name} has been defeated!");
        combatHandler.EndCombat();
    }
}
