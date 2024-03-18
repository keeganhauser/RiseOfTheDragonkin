using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : AnimationController
{
    [SerializeField] private Direction defaultDirection;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        SetAnimator(defaultDirection);
    }
}
