using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }


    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    [Range(1f, 10f)]
    private float xAttackRange = 1f;

    [SerializeField]
    public string Name { get; private set; } = "DefaultName";

    public bool CanMove { get; set; }

    private Vector2 movementVec;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool canInteract;
    private GameObject lastCollidedObj;

    private void Awake()
    {
        InstantiatePlayer();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SetAnimator(Direction.Right);
        canInteract = false;
        CanMove = true;
    }

    private void InstantiatePlayer() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            HandleInteractions();
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadSceneAsync("CombatScene");
    }

    private void OnMovement(InputValue value)
    {
        movementVec = value.Get<Vector2>();

        if (movementVec.x != 0 || movementVec.y != 0)
        {
            animator.SetFloat("X", movementVec.x);
            animator.SetFloat("Y", movementVec.y);

            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
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

    private void FixedUpdate()
    {
        if (CanMove)
            HandleMovement();
    }

    private void HandleInteractions()
    {
        if (!canInteract || lastCollidedObj is null) { return; }

        switch (lastCollidedObj.tag)
        {
            case "Door":
                Debug.Log("Interacted with door!");
                break;

            default:
                break;
        }
    }

    private void HandleMovement()
    {
        rb2d.MovePosition(rb2d.position + movementVec * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        canInteract = true;
        lastCollidedObj = collider.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == lastCollidedObj.tag)
        {
            canInteract = false;
        }
    }
}
