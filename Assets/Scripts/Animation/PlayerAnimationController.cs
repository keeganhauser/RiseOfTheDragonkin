using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    private PlayerMovementController movementController;

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPostInitialization += CombatStart;
        GameEventsManager.Instance.PlayerEvents.onPlayerDecideAttack += TriggerAttackAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerInitializeFinish += Initialize;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPostInitialization -= CombatStart;
        GameEventsManager.Instance.PlayerEvents.onPlayerDecideAttack -= TriggerAttackAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerInitializeFinish -= Initialize;
    }

    private void Initialize()
    {
        movementController = GetComponentInParent<PlayerMovementController>();
        animator = GetComponent<Animator>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    protected override void SetAnimator(Direction direction)
    {
        base.SetAnimator(direction);
        if (movementController.movementDisabled)
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
