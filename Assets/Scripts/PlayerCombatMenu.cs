using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatMenu : MonoBehaviour
{
    public void DisableCombatMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void EnableCombatMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
