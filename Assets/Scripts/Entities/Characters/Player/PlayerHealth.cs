using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerStats stats;

    private void Start()
    {
        stats = Player.Instance.Stats;
    }

    public void TakeDamage(float amount)
    {
        stats.Health = Mathf.Max(stats.Health - amount, 0f);

        if (stats.Health <= 0f) 
        {
            Die();
        }
    }

    private void Die()
    {
        GameEventsManager.Instance.PlayerEvents.PlayerDeath();
    }
}
