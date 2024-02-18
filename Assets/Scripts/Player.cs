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
        canInteract = false;
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
        //float attackX = (direction == Direction.Right) ? xAttackRange : -xAttackRange;
        //Collider2D hitEnemy = Physics2D.OverlapBox(transform.position, new Vector2(attackX, 1), 0f, LayerMask.GetMask("Enemy"));

        //if (hitEnemy != null)
        //{
        //    Destroy(hitEnemy);
        //    SceneManager.LoadScene("CombatScene");
        //}
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

    public void ZeroMovement()
    {
        rb2d.velocity = Vector3.zero;
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
