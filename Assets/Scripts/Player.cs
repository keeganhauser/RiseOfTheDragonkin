using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float speed = 1f;

    public int HP { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private bool canInteract;
    private GameObject lastCollidedObj;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        canInteract = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            HandleInteractions();
    }

    private void FixedUpdate()
    {
        HandleInput();
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


    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical"); 

        spriteRenderer.flipX = horizontalInput < 0f;

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
