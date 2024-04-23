using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    private PlayerStats stats;

    private void Start()
    {
        stats = Player.Instance.Stats;
    }

    public void UseMana(float amount)
    {
        stats.Mana = Mathf.Max(stats.Mana - amount, 0f);
    }
}
