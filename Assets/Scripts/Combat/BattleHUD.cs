using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Image hpImage;

    private Unit unit;

    public void RegisterUnit(Unit unit)
    {
        this.unit = unit;
        unit.OnHealthChange.AddListener(SetHP);
        SetHP();
        SetHUD();
    }

    public void SetHUD()
    {
        nameText.text = unit.UnitName;
        levelText.text = $"Lvl. {unit.UnitLevel}";
        hpImage.fillAmount = (float)unit.CurrentHealth / unit.MaxHealth;
    }

    private void SetHP()
    {
        hpImage.fillAmount = (float)unit.CurrentHealth / unit.MaxHealth;
    }
}
