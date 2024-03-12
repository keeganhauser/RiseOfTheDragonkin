using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHUDController : MonoBehaviour
{
    [SerializeField] private CombatHUD playerCombatHUD;
    [SerializeField] private CombatHUD enemyCombatHUD;

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPostInitialization += InitializeHUDS;
        GameEventsManager.Instance.CombatEvents.onHealthChange += UpdateHUD;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPostInitialization -= InitializeHUDS;
        GameEventsManager.Instance.CombatEvents.onHealthChange -= UpdateHUD;
    }

    private void InitializeHUDS()
    {
        playerCombatHUD.InitializeHUD(CombatManager.Instance.playerCombatController);
        enemyCombatHUD.InitializeHUD(CombatManager.Instance.enemyCombatController);
    }

    private void UpdateHUD(CombatController controller)
    {
        if (controller == CombatManager.Instance.playerCombatController)
            playerCombatHUD.SetHP(controller);
        else
            enemyCombatHUD.SetHP(controller);
    }
}
