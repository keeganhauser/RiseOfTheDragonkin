using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Image hpImage;

    public void InitializeHUD(CombatController controller)
    {
        if (controller == null)
        {
            Debug.LogWarning("Tried to init HUD but controller was null");
            return;
        }
        Debug.Log("Setting hud");
        nameText.text = controller.Name;
        levelText.text = $"Lvl. {controller.Level}";
        SetHP(controller);   
    }

    public void SetHP(CombatController controller)
    {
        hpImage.fillAmount = (float)controller.CurrentHealth / controller.MaxHealth;
    }
}
