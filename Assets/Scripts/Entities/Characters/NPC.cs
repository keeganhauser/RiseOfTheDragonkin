using UnityEngine;
using UnityEngine.Events;

public abstract class NPC : Entity, IInteractable
{
    [SerializeField] private Direction direction;
    [SerializeField] private SpriteRenderer interactSprite;

    private const float interactDistance = 2f;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.onInteractPressed += Interact;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.onInteractPressed -= Interact;
    }

    private void Update()
    {
        // Handle showing/hiding of interaction sprite
        if (interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(false);
        }
        else if (!interactSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(true);
        }
    }

    public abstract void Interact();

    protected bool IsWithinInteractDistance()
    {
        return Vector2.Distance(
            Player.Instance.transform.position,
            this.transform.position)
            < interactDistance;
    }
}
