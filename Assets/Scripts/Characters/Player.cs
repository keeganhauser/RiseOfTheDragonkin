using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

public class Player : MonoBehaviour
{
    // Static variables
    public static Player Instance;

    // Serialized variables
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    [Range(1f, 10f)]
    private float interactRange = 1f;

    [SerializeField]
    public string Name { get; private set; } = "DefaultName";

    // Public variables

    // Private variables
    private Vector2 movementVec;
    private Rigidbody2D rb2d;
    private Animator animator;

    // Properties
    private Direction playerDirection;
    public Direction PlayerDirection
    {
        get { return playerDirection; }
        set
        {
            playerDirection = value;
            SetAnimator(playerDirection);
        }
    }

    public bool CanMove { get; set; }


    // Private methods
    private void Awake()
    {
        InstantiatePlayer();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        PlayerDirection = Direction.Right;
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

    private void OnMovement(InputValue value)
    {
        if (!CanMove) return;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadSceneAsync("CombatScene");
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

        if (!CanMove)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if (CanMove)
            HandleMovement();
    }

    private void HandleMovement()
    {
        rb2d.MovePosition(rb2d.position + movementVec * speed * Time.fixedDeltaTime);
    }
}
