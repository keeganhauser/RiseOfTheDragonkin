using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : ConsumableItem
{
    [field: SerializeField] private int healAmount = 5;

    public override void Consume(GameObject target)
    {
        Debug.Log($"{target.GetComponent<CombatController>().Name} is using a healing potion");
        target.GetComponent<CombatController>().GainHealth(healAmount);
    }
}
