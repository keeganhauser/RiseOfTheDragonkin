using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb2d;
    private Vector2 velocity;
    private Animator animator;
    private bool movementDisabled;
    private Vector3 savedLocation;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        animator = GetComponentInChildren<Animator>();
        movementDisabled = false;
    }

    private void OnEnable()
    {
        Debug.Log($"{this.GetInstanceID()} Controller enabled");
        GameEventsManager.Instance.InputEvents.onMovePressed += MovePressed;
        GameEventsManager.Instance.PlayerEvents.onDisablePlayerMovement += DisablePlayerMovement;
        GameEventsManager.Instance.PlayerEvents.onEnablePlayerMovement += EnablePlayerMovement;
        GameEventsManager.Instance.CombatEvents.onCombatPreInitialization += SavePlayerLocation;
        GameEventsManager.Instance.CombatEvents.onCombatEnd += LoadPlayerLocation;
    }

    private void OnDisable()
    {
        Debug.Log($"{this.GetInstanceID()} Controller disabled");
        GameEventsManager.Instance.InputEvents.onMovePressed -= MovePressed;
        GameEventsManager.Instance.PlayerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
        GameEventsManager.Instance.PlayerEvents.onEnablePlayerMovement -= EnablePlayerMovement;
        GameEventsManager.Instance.CombatEvents.onCombatPreInitialization -= SavePlayerLocation;
        GameEventsManager.Instance.CombatEvents.onCombatEnd -= LoadPlayerLocation;
    }

    private void SavePlayerLocation()
    {
        savedLocation = this.transform.position;
    }

    private void LoadPlayerLocation()
    {
        this.transform.position = savedLocation;
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
        Debug.Log(movementDisabled);
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

