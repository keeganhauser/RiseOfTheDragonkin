using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerAnimationController : AnimationController
{
    private PlayerMovementController movementController;

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPostInitialization += CombatStart;
        GameEventsManager.Instance.PlayerEvents.onPlayerDecideAttack += TriggerAttackAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerInitializeFinish += Initialize;
        GameEventsManager.Instance.PlayerEvents.onPlayerWasHit += TriggerPlayerHitAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerDeath += TriggerPlayerDeadAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerRevive += TriggerPlayerReviveAnim;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPostInitialization -= CombatStart;
        GameEventsManager.Instance.PlayerEvents.onPlayerDecideAttack -= TriggerAttackAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerInitializeFinish -= Initialize;
        GameEventsManager.Instance.PlayerEvents.onPlayerWasHit -= TriggerPlayerHitAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerDeath -= TriggerPlayerDeadAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerRevive -= TriggerPlayerReviveAnim;

    }

    private void Initialize()
    {
        movementController = GetComponentInParent<PlayerMovementController>();
        animator = GetComponent<Animator>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void TriggerPlayerHitAnim()
    {
        StartCoroutine(TriggerAnimation("GotHit"));
    }

    private void TriggerPlayerDeadAnim()
    {
        StartCoroutine(TriggerAnimation("Died"));
    }

    private void TriggerPlayerReviveAnim()
    {
        StartCoroutine(TriggerAnimation("Revived"));
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
