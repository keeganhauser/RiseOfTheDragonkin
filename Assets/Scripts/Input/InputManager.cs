using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Wrapper class around Unity's Player Input component.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.started || context.performed || context.canceled)
        {
            GameEventsManager.Instance.InputEvents.MovePressed(context.ReadValue<Vector2>());
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.Instance.InputEvents.InteractPressed();
        }
    }

    public void QuestLogTogglePressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.Instance.InputEvents.QuestLogTogglePressed();
        }
    }

    public void InventoryTogglePressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.Instance.InputEvents.InventoryTogglePressed();
        }
    }

    public void ToolbarScroll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.Instance.InputEvents.ToolbarScroll(context.ReadValue<float>());
        }
    }

    public void PauseTogglePressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.Instance.InputEvents.Pause();
        }
    }

}
