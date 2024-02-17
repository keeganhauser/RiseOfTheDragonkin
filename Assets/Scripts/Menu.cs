using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public UnityEvent menuOpened = new UnityEvent();
    public UnityEvent menuClosed = new UnityEvent();

}
