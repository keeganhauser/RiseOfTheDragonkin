using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCombatBehavior :  CombatBehavior
{
    public override CombatAction? DecideCombatAction()
    {
        CombatAction? action;

        if (Random.value <= 0.5f)
            action = CombatAction.Attack;
        else
            action = CombatAction.Defend;

        return action;
    }
}
