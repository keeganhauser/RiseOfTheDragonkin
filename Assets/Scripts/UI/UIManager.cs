using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonoBehavior<UIManager>
{
    [SerializeField] private GameObject pauseMenu;
   
    public bool PauseMenuOn { get; private set; }
    

    protected override void Awake()
    {
        base.Awake();

        PauseMenuOn = false;
        pauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.onPause += TogglePauseMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.onPause -= TogglePauseMenu;
    }

    private void Update()
    {
        if (PauseMenuOn)
        {
            
        }
    }

    private void TogglePauseMenu()
    {
        if (PauseMenuOn)
            DisablePauseMenu();
        else
            EnablePauseMenu();
    }

    private void DisablePauseMenu()
    {
        PauseMenuOn = false;
        GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
        Time.timeScale = 1;
        pauseMenu.SetActive(PauseMenuOn);
    }

    private void EnablePauseMenu()
    {
        PauseMenuOn = true;
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
        Time.timeScale = 0;
        pauseMenu.SetActive(PauseMenuOn);

        //System.GC.Collect();
    }
}
