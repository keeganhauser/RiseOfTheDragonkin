using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private List<Menu> menus = new List<Menu>();

    private void Start()
    {
        ShowMenu(menus[0]);
    }

    public void ShowMenu(Menu menu)
    {
        if (!menus.Contains(menu))
        {
            Debug.LogError($"{menu.name} is not found in the list of menus.");
            return;
        }

        foreach (Menu m in menus)
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
