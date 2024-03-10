using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO: Move enums
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum CharacterAction
{
    Idle,
    Move,
    Attack
}

public class PlayerMovementController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb2d;
    private Vector2 velocity;
    private Animator animator;
    private bool movementDisabled;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        animator = GetComponentInChildren<Animator>();
        movementDisabled = false;
    }

    private void Start()
    {
        GameEventsManager.Instance.InputEvents.onMovePressed += MovePressed;
        GameEventsManager.Instance.PlayerEvents.onDisablePlayerMovement += DisablePlayerMovement;
        GameEventsManager.Instance.PlayerEvents.onEnablePlayerMovement += EnablePlayerMovement;
    }

    private void OnDestroy()
    {
        GameEventsManager.Instance.InputEvents.onMovePressed -= MovePressed;
        GameEventsManager.Instance.PlayerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
        GameEventsManager.Instance.PlayerEvents.onEnablePlayerMovement -= EnablePlayerMovement;
    }

    private void DisablePlayerMovement()
    {
        movementDisabled = true;
    }

    private void EnablePlayerMovement()
    {
        movementDisabled = false;
    }

    private void MovePressed(Vector2 moveDir)
    {
        velocity = moveDir.normalized * moveSpeed;

        if (movementDisabled)
        {
            velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = velocity;
    }

    private void UpdateAnimations()
    {
        // Update animator parameters
        if (velocity.x != 0 || velocity.y != 0)
        {
            animator.SetFloat("X", velocity.x);
            animator.SetFloat("Y", velocity.y);

            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public IEnumerator TriggerAnimation(CharacterAction action)
    {
        switch (action)
        {
            case CharacterAction.Attack:
                animator.SetTrigger("Attack");
                yield return new WaitForSeconds(0.5f);
                animator.ResetTrigger("Attack");
                break;
            default:
                break;
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

        if (movementDisabled)
        {
            animator.SetBool("IsMoving", false);
        }
    }
}

