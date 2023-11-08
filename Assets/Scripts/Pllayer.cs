using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pllayer : MonoBehaviour
{

    [SerializeField]
    private float speed = 1f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
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
            * speed;

        rb2d.velocity = moveVec;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

}
