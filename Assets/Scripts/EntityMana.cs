using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMana : MonoBehaviour
{
    [SerializeField]
    private int mana = 100;
    public int Mana => mana;

    [SerializeField]
    private int maxMana = 100;
    public int MaxMana => maxMana;

    public void LoseMana(int amount)
    {
        mana -= amount;
    }

    public void GainMana(int amount)
    {
        mana = (int)Mathf.Clamp(mana + amount, 0f, maxMana);
    }

    public void IncreaseMaxMana(int amount)
    {
        maxMana += amount;
    }
}
