using UnityEngine;
using UnityEngine.Events;

public abstract class NPC : Entity, IInteractable
{
    [SerializeField] private Direction direction;
    [SerializeField] private SpriteRenderer interactSprite;

    private Animator animator;
    private const float interactDistance = 2f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        SetAnimator(direction);
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

    private void SetAnimator(Direction direction)
    {
        float x = 0f;
        float y = 0f;

        switch (direction)
        {
            case Direction.Up:
                y = 1f;
                break;
            case Direction.Down:
                y = -1f;
                break;
            case Direction.Left:
                x = -1f;
                break;
            case Direction.Right:
                x = 1f;
                break;
            default:
                break;
        }

        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
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
