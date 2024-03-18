using System;
using UnityEngine;

public class InputEvents
{
    public event Action<Vector2> onMovePressed;
    public void MovePressed(Vector2 moveDir)
    {
        onMovePressed?.Invoke(moveDir);
    }

    public event Action onInteractPressed;
    public void InteractPressed()
    {
        onInteractPressed?.Invoke();
    }

    public event Action onInventoryTogglePressed;
    public void InventoryTogglePressed()
    {
        onInventoryTogglePressed?.Invoke();
    }

    public event Action onQuestLogTogglePressed;
    public void QuestLogTogglePressed()
    {
        onQuestLogTogglePressed?.Invoke();
    }

    public event Action<float> onToolbarScroll;
    public void ToolbarScroll(float scroll)
    {
        onToolbarScroll?.Invoke(scroll);
    }

    public event Action onPause;
    public void Pause()
    {
        onPause?.Invoke();
    }
}
