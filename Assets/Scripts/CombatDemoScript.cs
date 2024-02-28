using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDemoScript : MonoBehaviour
{
    public Enemy enemy;

    public void StartCombat()
    {
        CombatManager.Instance.StartEncounter(enemy);
    }
}
