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
    private bool inCombat;
    private bool isDefending = false;

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
        inCombat = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            HandleInteractions();
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }

    public void CombatAttack()
    {
        FindObjectOfType<Enemy>().TakeDamage(damage);
        FindObjectOfType<CombatHandler>().EndTurn();
    }

    public void Defend()
    {
        isDefending = true;
        FindObjectOfType<CombatHandler>().EndTurn();
    }

    private void Attack()
    {
        float attackX = (direction == Direction.Right) ? xAttackRange : -xAttackRange;
        Collider2D hitEnemy = Physics2D.OverlapBox(transform.position, new Vector2(attackX, 1), 0f, LayerMask.GetMask("Enemy"));

        if (hitEnemy != null)
            EnterCombat(hitEnemy);
    }

    private void EnterCombat(Collider2D enemy)
    {
        inCombat = true;
        DontDestroyOnLoad(enemy.gameObject);

        SceneManager.LoadScene("CombatScene");
    }

    public void LeaveCombat()
    {
        inCombat = false;
    }

    private void FixedUpdate()
    {
        if (!inCombat)
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

    public void TakeDamage(int damage)
    {
        if (isDefending)
        {
            damage /= 2;
            isDefending = false;
        }
        HP -= damage;
        Debug.Log($"{Name} took {damage} damage!");

        if (HP <= 0)
        {
            Die();
            Debug.Log("Player has died!");
        }
    }

    private void Die()
    {
        // TODO: Add in stuff for when the player loses all hp
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

    public void ResetPosition()
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
