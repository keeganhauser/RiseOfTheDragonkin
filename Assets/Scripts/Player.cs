using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum Direction
    {
        Left = -1,
        Right = 1
    }


    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private int damage = 10;

    [SerializeField]
    [Range(1f, 10f)]
    private float xAttackRange = 1f;

    [SerializeField]
    public string Name { get; private set; } = "DefaultName";

    public int HP { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private bool canInteract;
    private GameObject lastCollidedObj;
    private Direction direction;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        canInteract = false;
        direction = Direction.Right;
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
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(attackX, 1), 0f, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in  hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void FixedUpdate()
    {
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
