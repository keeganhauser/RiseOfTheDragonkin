using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    private Unit unit;

    public void RegisterUnit(Unit unit)
    {
        this.unit = unit;
        unit.OnHealthChange.AddListener(SetHP);
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.UnitName;
        levelText.text = $"Lvl. {unit.UnitLevel}";
        hpSlider.maxValue = unit.MaxHealth;
        hpSlider.value = unit.CurrentHealth;
    }

    private void SetHP()
    {
        hpSlider.value = unit.CurrentHealth;
    }
}
