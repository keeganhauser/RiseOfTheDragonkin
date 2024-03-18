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
    public bool movementDisabled { get; private set; }
    private Vector3 savedLocation;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        movementDisabled = false;
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.onMovePressed += MovePressed;
        GameEventsManager.Instance.PlayerEvents.onDisablePlayerMovement += DisablePlayerMovement;
        GameEventsManager.Instance.PlayerEvents.onEnablePlayerMovement += EnablePlayerMovement;
        GameEventsManager.Instance.CombatEvents.onCombatPrePreInitialization += SavePlayerLocation;
        GameEventsManager.Instance.CombatEvents.onCombatEnd += LoadPlayerLocation;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.onMovePressed -= MovePressed;
        GameEventsManager.Instance.PlayerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
        GameEventsManager.Instance.PlayerEvents.onEnablePlayerMovement -= EnablePlayerMovement;
        GameEventsManager.Instance.CombatEvents.onCombatPrePreInitialization -= SavePlayerLocation;
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
        velocity = Vector2.zero;
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

    private void FixedUpdate()
    {
        rb2d.velocity = velocity;
    }
}

