using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : ConsumableItem
{
    [SerializeField] private int manaGainAmount = 5;
    public override void Consume(GameObject target)
    {
        target.GetComponent<CombatController>().GainMana(manaGainAmount);
    }
}
