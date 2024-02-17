using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private bool showOnStartup = false;

    [SerializeField]
    private List<Menu> menusList = new List<Menu>();

    private void Start()
    {
        if (showOnStartup)
            ShowMenu(menusList[0]);
    }

    public void ShowMenu(Menu menu)
    {
        if (!menusList.Contains(menu))
        {
            Debug.LogError($"{menu.name} is not found in the list of menus.");
            return;
        }

        foreach (Menu m in menusList)
        {
            if (m == menu)
            {
                m.gameObject.SetActive(true);
            }
            else
            {
                if (m.gameObject.activeInHierarchy)
                {
                    m.menuClosed.Invoke();
                }
                m.gameObject.SetActive(false);
            }
        }
    }
}
