using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    enum Direction
    {
        Left = -1,
        Right = 1
    }


    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    [Range(1f, 10f)]
    private float xAttackRange = 1f;

    [SerializeField]
    public string Name { get; private set; } = "DefaultName";

    public bool CanMove { get; set; }

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private bool canInteract;
    private GameObject lastCollidedObj;
    private Direction direction;

    private void Awake()
    {
        InstantiatePlayer();
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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        canInteract = false;
        direction = Direction.Right;
        CanMove = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            HandleInteractions();
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }

    private void Attack()
    {
        float attackX = (direction == Direction.Right) ? xAttackRange : -xAttackRange;
        Collider2D hitEnemy = Physics2D.OverlapBox(transform.position, new Vector2(attackX, 1), 0f, LayerMask.GetMask("Enemy"));

        if (hitEnemy != null)
        {
            Destroy(hitEnemy);
            SceneManager.LoadScene("CombatScene");
        }
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        direction = (horizontalInput < 0f) ? Direction.Left : Direction.Right;
        
        spriteRenderer.flipX = direction == Direction.Left;

        Vector3 moveVec = new Vector3(
            horizontalInput,
            verticalInput,
            0)
            * speed
            * Time.fixedDeltaTime;

        rb2d.velocity = moveVec;
    }

    public void ZeroMovement()
    {
        rb2d.velocity = Vector3.zero;
        direction = Direction.Right;
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
